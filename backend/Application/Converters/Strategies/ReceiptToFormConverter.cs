using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Helpers.Reducers;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Converters.Strategies;

public class ReceiptToFormConverter(ISolutionDbContext dbContext, IDecimalParser decimalParser) : IReceiptToFormConverter
{
    private ISolutionDbContext _dbContext = dbContext;
    private ValuesReducerStrategyFactory _valueMergeStrategyFactory = new ValuesReducerStrategyFactory(decimalParser);

    public bool CanConvert(Type sourceType, Type targetType)
        => sourceType == typeof(Receipt) && targetType == typeof(FormTemplate);

    public async Task<Record> Convert(Record source, RecordTemplate target, CancellationToken cancellationToken)
    {
        if (source is not Receipt receipt || target is not FormTemplate formTemplate)
        {
            throw new InvalidOperationException("Invalid types for conversion.");
        }

        receipt = await _dbContext.Receipts
            .Include(l => l.RecordTemplate)
            .Include(t => t.Values)
                .ThenInclude(s => s.Field)
            .FirstAsync(r => r.Id == receipt.Id, cancellationToken);

        formTemplate = await _dbContext.FormTemplates
            .FirstAsync(r => r.Id == formTemplate.Id, cancellationToken);

        var receiptFormLink = await _dbContext.RecordTemplateLinks
            .Include(l => l.FormTemplate)
                .ThenInclude(t => t.Sections)
                    .ThenInclude(s => s.Fields)
            .Include(l => l.ReceiptTemplate)
                .ThenInclude(t => t.Sections)
                    .ThenInclude(s => s.Fields)
            .Include(l => l.RecordFieldLinks)
                .ThenInclude(l => l.FormField)
            .Include(l => l.RecordFieldLinks)
                .ThenInclude(l => l.ReceiptField)
            .SingleAsync(o => o.ReceiptTemplateId == receipt.RecordTemplateId &&
                o.FormTemplateId == formTemplate.Id, cancellationToken);

        var fieldsRetenciones = receipt.RecordTemplate.Sections
            .Where(s => string.Equals("Retenciones", s.Name, StringComparison.InvariantCultureIgnoreCase))
            .SelectMany(s => s.Fields)
            .Select(s => s.Id)
            .ToArray();

        var fieldsHaberes = receipt.RecordTemplate.Sections
            .Where(s => !string.Equals("Retenciones", s.Name, StringComparison.InvariantCultureIgnoreCase))
            .SelectMany(s => s.Fields)
            .Select(s => s.Id)
            .ToArray();

        var valueMergeStrategy = _valueMergeStrategyFactory.GetStrategy(FieldType.Number)!;

        var totalRetenciones = (decimal)valueMergeStrategy.Reduce(receipt.Values.Where(v => fieldsRetenciones.Contains(v.FieldId)).Select(v => v.Value).ToArray());
        var totalHaberes = (decimal)valueMergeStrategy.Reduce(receipt.Values.Where(v => fieldsHaberes.Contains(v.FieldId)).Select(v => v.Value).ToArray());
        var neto = totalHaberes - totalRetenciones;

        var resultValues = receiptFormLink.RecordFieldLinks.GroupBy(rfl => rfl.FormFieldId)
            .Select(g =>
            {
                var formField = g.First().FormField;
                var receiptFields = g.Select(g1 => g1.ReceiptField).ToArray();

                return new FormValue
                {
                    FieldId = formField.Id,
                    Value = ProcessValues(formField, receiptFields, receipt.Values)
                };
            }).ToArray();

        var newForm = new Form
        {
            Title = receipt.Title,
            RecordTemplate = formTemplate,
            Values = resultValues
        };

        newForm.ReceiptToFormConversions = [
                new ReceiptToFormConversion
                {
                    RecordTemplateLink = receiptFormLink!,
                    Source = receipt,
                    Target = newForm
                }];

        return newForm;
    }

    private string ProcessValues(FormField formField, ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values)
    {
        var valueMergeStrategy = _valueMergeStrategyFactory.GetStrategy(formField.FieldType)!;
        return (valueMergeStrategy.Reduce(receiptFields, values) ?? 0).ToString()!;
    }
}

public interface IReceiptToFormConverter : IRecordConverterStrategy
{
}
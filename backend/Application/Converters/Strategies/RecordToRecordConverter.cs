using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Helpers.Reducers;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Converters.Strategies;

public class RecordToRecordConverter(ISolutionDbContext dbContext, IDecimalParser decimalParser) : IRecordToRecordConverter
{
    private ISolutionDbContext _dbContext = dbContext;
    private ValuesReducerStrategyFactory _valueMergeStrategyFactory = new ValuesReducerStrategyFactory(decimalParser);

    public bool CanConvert(Type sourceType, Type targetType)
        => sourceType == typeof(Record) && targetType == typeof(RecordTemplate);

    public async Task<Record> Convert(Record source, RecordTemplate target, CancellationToken cancellationToken)
    {
        var sourceRecord = await _dbContext.Records
            .Include(l => l.Template)
            .Include(t => t.Values)
                .ThenInclude(s => s.Field)
            .FirstAsync(r => r.Id == source.Id, cancellationToken);

        var targetTemplate = await _dbContext.RecordTemplates
            .FirstAsync(r => r.Id == target.Id, cancellationToken);

        var recordTemplateLink = await _dbContext.RecordTemplateLinks
            .Include(l => l.RightTemplate)
                .ThenInclude(t => t.Sections)
                    .ThenInclude(s => s.Fields)
            .Include(l => l.LeftTemplate)
                .ThenInclude(t => t.Sections)
                    .ThenInclude(s => s.Fields)
            .Include(l => l.RecordFieldLinks)
                .ThenInclude(l => l.RightField)
            .Include(l => l.RecordFieldLinks)
                .ThenInclude(l => l.LeftField)
            .SingleAsync(o => o.LeftTemplateId == sourceRecord.TemplateId &&
                o.RightTemplateId == targetTemplate.Id, cancellationToken);

        var fieldsRetenciones = sourceRecord.Template.Sections
            .Where(s => string.Equals("Retenciones", s.Name, StringComparison.InvariantCultureIgnoreCase))
            .SelectMany(s => s.Fields)
            .Select(s => s.Id)
            .ToArray();

        var fieldsHaberes = sourceRecord.Template.Sections
            .Where(s => !string.Equals("Retenciones", s.Name, StringComparison.InvariantCultureIgnoreCase))
            .SelectMany(s => s.Fields)
            .Select(s => s.Id)
            .ToArray();

        var valueMergeStrategy = _valueMergeStrategyFactory.GetStrategy(FieldType.Number)!;

        var totalRetenciones = (decimal)valueMergeStrategy.Reduce(sourceRecord.Values.Where(v => fieldsRetenciones.Contains(v.FieldId)).Select(v => v.Value).ToArray());
        var totalHaberes = (decimal)valueMergeStrategy.Reduce(sourceRecord.Values.Where(v => fieldsHaberes.Contains(v.FieldId)).Select(v => v.Value).ToArray());
        var neto = totalHaberes - totalRetenciones;

        var resultValues = recordTemplateLink.RecordFieldLinks.GroupBy(rfl => rfl.RightFieldId)
            .Select(g =>
            {
                var rightField = g.First().RightField;
                var leftFields = g.Select(g1 => g1.LeftField).ToArray();

                return new RecordValue
                {
                    FieldId = rightField.Id,
                    Value = ProcessValues(rightField, leftFields, sourceRecord.Values)
                };
            }).ToArray();

        var newRecord = new Record
        {
            Title = sourceRecord.Title,
            Template = targetTemplate,
            Values = resultValues
        };

        newRecord.ConvertedFrom = [
                new RecordTemplateConversion
                {
                    RecordTemplateLink = recordTemplateLink!,
                    Source = sourceRecord,
                    Target = newRecord
                }];

        return newRecord;
    }

    private string ProcessValues(RecordTemplateField rightField, ICollection<RecordTemplateField> leftFields, ICollection<RecordValue> values)
    {
        var valueMergeStrategy = _valueMergeStrategyFactory.GetStrategy(rightField.FieldType)!;
        return (valueMergeStrategy.Reduce(leftFields, values) ?? 0).ToString()!;
    }
}

public interface IRecordToRecordConverter : IRecordConverterStrategy
{
}
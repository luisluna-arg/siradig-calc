using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Core.Entities.Receipts;
using ConversionFieldValueDto = SiradigCalc.Application.Dtos.Conversion.FieldValueDto;
using SiradigCalc.Application.Helpers.Reducers;
using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Application.Mappers;

public class ReceiptToFormConversionMapper(IDtoMappingService dtoMappingService)
    : BaseMapper<ReceiptToFormConversion, ReceiptToFormConversionDto>(dtoMappingService),
    IRecordConversionMapper
{
    private ValuesReducerStrategyFactory _valueMergeStrategyFactory = new ValuesReducerStrategyFactory(new DecimalParserStrategy());
    public override ReceiptToFormConversionDto Map(ReceiptToFormConversion recordConvertion)
    {
        var receipt = recordConvertion.Source;
        
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

        var resultValues = recordConvertion.RecordTemplateLink.RecordFieldLinks.GroupBy(rfl => rfl.FormFieldId)
            .Select(g =>
            {
                var formField = g.First().FormField;
                var receiptFields = g.Select(g1 => g1.ReceiptField).ToArray();

                return new ConversionFieldValueDto()
                {
                    FieldId = formField.Id,
                    Label = formField.Label,
                    FieldType = formField.FieldType,
                    IsRequired = formField.IsRequired,
                    Value = ProcessValues(formField, receiptFields, receipt!.Values)
                };
            }).ToArray();

        return new ReceiptToFormConversionDto()
        {
            Id = recordConvertion.Id,
            RecordTemplateLinkId = recordConvertion.RecordTemplateLinkId,
            RecordTemplateLink = DtoMappingService.Map<RecordTemplateLinkBasicDto>(recordConvertion.RecordTemplateLink),
            SourceId = recordConvertion.SourceId,
            Source = DtoMappingService.Map<RecordDto>(recordConvertion.Source),
            TargetId = recordConvertion.TargetId,
            Target = DtoMappingService.Map<RecordDto>(recordConvertion.Target),
            Values = resultValues,
            Retenciones = totalRetenciones,
            Haberes = totalHaberes,
            Neto = neto
        };
    }

    private object ProcessValues(FormField formField, ICollection<ReceiptField> receiptFields, ICollection<ReceiptValue> values)
    {
        var valueMergeStrategy = _valueMergeStrategyFactory.GetStrategy(formField.FieldType)!;
        return valueMergeStrategy.Reduce(receiptFields, values);
    }
}

public interface IRecordConversionMapper : IDtoMapper<ReceiptToFormConversion, ReceiptToFormConversionDto>
{
}
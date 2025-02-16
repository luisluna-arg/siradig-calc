using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Helpers.Reducers;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordTemplateConversionMapper(IDtoMappingService dtoMappingService)
    : BaseMapper<RecordConversion, RecordTemplateConversionDto>(dtoMappingService),
    IRecordTemplateConversionMapper
{
    private ValuesReducerStrategyFactory _valueMergeStrategyFactory = new ValuesReducerStrategyFactory(new DecimalParserStrategy());

    public override RecordTemplateConversionDto Map(RecordConversion recordConvertion)
    {
        var recordSource = recordConvertion.Source;

        var fieldsRetenciones = recordSource.Template.Sections
            .Where(s => string.Equals("Retenciones", s.Name, StringComparison.InvariantCultureIgnoreCase))
            .SelectMany(s => s.Fields)
            .Select(s => s.Id)
            .ToArray();

        var fieldsHaberes = recordSource.Template.Sections
            .Where(s => !string.Equals("Retenciones", s.Name, StringComparison.InvariantCultureIgnoreCase))
            .SelectMany(s => s.Fields)
            .Select(s => s.Id)
            .ToArray();

        var valueMergeStrategy = _valueMergeStrategyFactory.GetStrategy(FieldType.Number)!;

        var totalRetenciones = (decimal)valueMergeStrategy.Reduce(recordSource.Values.Where(v => fieldsRetenciones.Contains(v.FieldId)).Select(v => v.Value).ToArray());
        var totalHaberes = (decimal)valueMergeStrategy.Reduce(recordSource.Values.Where(v => fieldsHaberes.Contains(v.FieldId)).Select(v => v.Value).ToArray());
        var neto = totalHaberes - totalRetenciones;

        var resultValues = recordConvertion.RecordTemplateLink.RecordFieldLinks
            .GroupBy(rfl => rfl.RightFieldId)
            .Select(g =>
            {
                var rightField = g.First().RightField;
                var leftFields = g.Select(g1 => g1.LeftField).ToArray();

                return new FieldValueDto()
                {
                    FieldId = rightField.Id,
                    Label = rightField.Label,
                    FieldType = rightField.FieldType,
                    IsRequired = rightField.IsRequired,
                    Value = ProcessValues(rightField, leftFields, recordSource!.Values)
                };
            }).ToArray();

        return new RecordTemplateConversionDto()
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

    private object ProcessValues(RecordTemplateField rightField, ICollection<RecordTemplateField> leftFields, ICollection<RecordValue> values)
    {
        var valueMergeStrategy = _valueMergeStrategyFactory.GetStrategy(rightField.FieldType)!;
        return valueMergeStrategy.Reduce(leftFields, values);
    }
}

public interface IRecordTemplateConversionMapper : IDtoMapper<RecordConversion, RecordTemplateConversionDto>
{
}
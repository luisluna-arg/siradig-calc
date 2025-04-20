using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Helpers.Reducers;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordTemplateConversionCompactMapper(IDtoMappingService dtoMappingService)
    : BaseMapper<RecordConversion, RecordTemplateConversionCompactDto>(dtoMappingService),
    IRecordTemplateConversionCompactMapper
{
    private ValuesReducerStrategyFactory _valueMergeStrategyFactory = new ValuesReducerStrategyFactory(new DecimalParserStrategy());

    public override RecordTemplateConversionCompactDto Map(RecordConversion recordConvertion)
    {
        return new RecordTemplateConversionCompactDto()
        {
            Id = recordConvertion.Id,
            RecordTemplateLinkId = recordConvertion.RecordTemplateLinkId,
            RecordTemplateLink = DtoMappingService.Map<RecordTemplateLinkBasicDto>(recordConvertion.RecordTemplateLink),
            Source = DtoMappingService.Map<RecordDto>(recordConvertion.Source),
            Target = DtoMappingService.Map<RecordDto>(recordConvertion.Target)
        };
    }

    private object ProcessValues(RecordTemplateField rightField, ICollection<RecordTemplateField> leftFields, ICollection<RecordValue> values)
    {
        var valueMergeStrategy = _valueMergeStrategyFactory.GetStrategy(rightField.FieldType)!;
        return valueMergeStrategy.Reduce(leftFields, values);
    }
}

public interface IRecordTemplateConversionCompactMapper : IDtoMapper<RecordConversion, RecordTemplateConversionCompactDto>
{
}
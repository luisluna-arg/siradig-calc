using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordTemplateFieldMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<RecordTemplateField, RecordTemplateFieldDto>(dtoMapperManager),
    IRecordTemplateFieldMapper
{
    public override RecordTemplateFieldDto Map(RecordTemplateField source)
        => new RecordTemplateFieldDto()
        {
            Id = source.Id,
            Label = source.Label,
            FieldType = DtoMappingService.Map<FieldTypeDto>(source.FieldType),
            IsRequired = source.IsRequired
        };
}

public interface IRecordTemplateFieldMapper : IDtoMapper<RecordTemplateField, RecordTemplateFieldDto>
{
}
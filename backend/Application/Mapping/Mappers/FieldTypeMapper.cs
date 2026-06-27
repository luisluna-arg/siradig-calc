using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Application.Mappers;

public interface IFieldTypeMapper : IDtoMapper<FieldType, FieldTypeDto>;

public class FieldTypeMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<FieldType, FieldTypeDto>(dtoMapperManager),
    IFieldTypeMapper
{
    public override FieldTypeDto Map(FieldType source)
        => new FieldTypeDto()
        {
            Id = (short)source,
            Name = source.ToString()
        };
}
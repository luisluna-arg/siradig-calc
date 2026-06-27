using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Enums;

namespace SiradigCalc.Application.Mappers;

public interface IFieldTypeCatalogMapper : IDtoMapper<FieldType, CatalogDto<short>>;

public class FieldTypeCatalogMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<FieldType, CatalogDto<short>>(dtoMapperManager),
    IFieldTypeCatalogMapper
{
    public override CatalogDto<short> Map(FieldType source)
        => new CatalogDto<short>()
        {
            Id = (short)source,
            Label = source.ToString()
        };

    public override IEnumerable<CatalogDto<short>> Map(IEnumerable<FieldType> source)
        => source.Select(Map).ToList();
}
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class TemplateCatalogMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<RecordTemplate, CatalogDto>(dtoMapperManager),
    ITemplateCatalogMapper
{
    public override CatalogDto Map(RecordTemplate source)
        => new CatalogDto()
        {
            Id = source.Id,
            Label = source.Name
        };

    public override IEnumerable<CatalogDto> Map(IEnumerable<RecordTemplate> source)
        => source.Select(Map).ToList();
}

public interface ITemplateCatalogMapper : IDtoMapper<RecordTemplate, CatalogDto>
{
}
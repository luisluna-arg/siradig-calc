using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping.Mappers.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;

namespace SiradigCalc.Application.Mappers;

public class RecordCatalogMapper(IDtoMappingService dtoMapperManager)
    : BaseMapper<Record, CatalogDto>(dtoMapperManager),
    IRecordCatalogMapper
{
    public override CatalogDto Map(Record source)
        => new CatalogDto()
        {
            Id = source.Id,
            Label = $"{source.Title} ({source.Template.Name})"
        };

    public override IEnumerable<CatalogDto> Map(IEnumerable<Record> source)
        => source.Select(Map).ToList();
}

public interface IRecordCatalogMapper : IDtoMapper<Record, CatalogDto>
{
}
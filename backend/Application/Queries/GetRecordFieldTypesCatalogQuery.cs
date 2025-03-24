using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordFieldTypesCatalogQuery() : IRequest<ICollection<CatalogDto<short>>>
{
}

public class GetRecordFieldTypesCatalogQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : GetCatalogBaseQueryHandler<GetRecordFieldTypesCatalogQuery, FieldType, CatalogDto<short>>(dbContext, dtoMappingService)
{
    protected override async Task<IEnumerable<FieldType>> GetData(GetRecordFieldTypesCatalogQuery query, CancellationToken cancellationToken)
        => await Task.FromResult(Enum.GetValues(typeof(FieldType)).Cast<FieldType>());
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetTemplateCatalogQuery() : IRequest<ICollection<Dtos.CatalogDto>>
{
}

public class GetTemplateCatalogQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : GetCatalogBaseQueryHandler<GetTemplateCatalogQuery, RecordTemplate>(dbContext, dtoMappingService)
{
    protected override async Task<IEnumerable<RecordTemplate>> GetData(GetTemplateCatalogQuery query, CancellationToken cancellationToken)
        => await DbContext.RecordTemplates
            .AsNoTracking()
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordCatalogQuery() : IRequest<ICollection<Dtos.CatalogDto>>
{
}

public class GetRecordCatalogQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : GetCatalogBaseQueryHandler<GetRecordCatalogQuery, Record, CatalogDto>(dbContext, dtoMappingService)
{
    protected override async Task<IEnumerable<Record>> GetData(GetRecordCatalogQuery query, CancellationToken cancellationToken)
        => await DbContext.Records
            .AsNoTracking()
            .Include(r => r.Template)
            .OrderBy(t => t.Template.Name)
                .ThenBy(t => t.Title)
            .ToListAsync(cancellationToken);
}
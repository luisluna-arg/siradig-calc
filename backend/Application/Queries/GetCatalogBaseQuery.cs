using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Base;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public abstract class GetCatalogBaseQueryHandler<TQuery, TEntity>(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : IRequestHandler<TQuery, ICollection<CatalogDto>>
    where TQuery : IRequest<ICollection<CatalogDto>>
    where TEntity : IEntity
{
    protected ISolutionDbContext DbContext { get; set; } = dbContext;
    protected IDtoMappingService DtoMappingService { get; set; } = dtoMappingService;

    public async virtual Task<ICollection<CatalogDto>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var data = await GetData(query, cancellationToken);
        /* TODO: Why doesn't the collection work */
        return data.Select(d => DtoMappingService.Map<CatalogDto>(d)).ToList();
    }

    protected abstract Task<IEnumerable<TEntity>> GetData(TQuery query, CancellationToken cancellationToken);
}
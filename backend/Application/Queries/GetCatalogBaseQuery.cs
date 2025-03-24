using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public abstract class GetCatalogBaseQueryHandler<TQuery, TEntity, TCatalog>(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : IRequestHandler<TQuery, ICollection<TCatalog>>
    where TQuery : IRequest<ICollection<TCatalog>>
    where TCatalog : ICatalog
    where TEntity : notnull
{
    protected ISolutionDbContext DbContext { get; set; } = dbContext;
    protected IDtoMappingService DtoMappingService { get; set; } = dtoMappingService;

    public async virtual Task<ICollection<TCatalog>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var data = await GetData(query, cancellationToken);
        /* TODO: Why doesn't the collection work */
        return data.Select(d => DtoMappingService.Map<TCatalog>(d)).ToList();
    }

    protected abstract Task<IEnumerable<TEntity>> GetData(TQuery query, CancellationToken cancellationToken);
}
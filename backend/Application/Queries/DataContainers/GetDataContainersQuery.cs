using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.DataContainers;

public abstract class GetDataContainersQuery<TDataContainerTemplate> : IRequest<IEnumerable<TDataContainerTemplate>>
{
}

public abstract class GetDataContainersQueryHandler<TQuery, TDataContainerTemplate, TDataContainerSection, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, IEnumerable<TDataContainerTemplate>>
    where TQuery : GetDataContainersQuery<TDataContainerTemplate>
    where TField : BaseDataContainerField, new()
    where TDataContainerSection : BaseDataContainerSection<TField>, new()
    where TDataContainerTemplate : BaseDataContainer<TDataContainerSection, TField>, new()
{
    public async virtual Task<IEnumerable<TDataContainerTemplate>> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TDataContainerTemplate>()
            .Include(d => d.Sections)
                .ThenInclude(d => d.Fields)
            .ToArrayAsync();
}

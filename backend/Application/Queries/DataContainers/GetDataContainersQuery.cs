using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.DataContainers;

public abstract class GetDataContainersQuery<TDataContainerTemplate> : IRequest<IEnumerable<TDataContainerTemplate>>
{
}

public abstract class GetDataContainersQueryHandler<TQuery, TDataContainerTemplate, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, IEnumerable<TDataContainerTemplate>>
    where TQuery : GetDataContainersQuery<TDataContainerTemplate>
    where TField : BaseDataContainerField, new()
    where TDataContainerTemplate : BaseDataContainer<TField>, new()
{
    public async virtual Task<IEnumerable<TDataContainerTemplate>> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TDataContainerTemplate>()
            .Include(d => d.Fields)
            .ToArrayAsync();
}

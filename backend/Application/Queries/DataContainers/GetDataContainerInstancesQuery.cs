using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.DataContainers;

public abstract class GetDataContainerInstancesQuery<TDataContainerInstance> : IRequest<IEnumerable<TDataContainerInstance>>
{
}

public abstract class GetDataContainerInstancesQueryHandler<TQuery, TDataContainerInstance, TField, TFieldValue, TDataContainerTemplate, TDataContainerSection>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, IEnumerable<TDataContainerInstance>>
    where TQuery : GetDataContainerInstancesQuery<TDataContainerInstance>
    where TField : BaseDataContainerField, new()
    where TFieldValue : BaseDataContainerValue<TField>, new()
    where TDataContainerSection : BaseDataContainerSection<TField>, new()
    where TDataContainerTemplate : BaseDataContainer<TDataContainerSection, TField>, new()
    where TDataContainerInstance : BaseDataContainerInstance<TDataContainerTemplate, Guid, TDataContainerSection, TFieldValue, TField>, new()
{
    public async virtual Task<IEnumerable<TDataContainerInstance>> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TDataContainerInstance>().ToArrayAsync();
}

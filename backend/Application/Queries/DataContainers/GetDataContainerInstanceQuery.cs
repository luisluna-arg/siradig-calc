using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.DataContainers;

public abstract class GetDataContainerInstanceQuery<TDataContainerInstance>(Guid id) : IRequest<TDataContainerInstance?>
{
    public Guid Id { get; set; } = id;
}

public abstract class GetDataContainerInstanceQueryHandler<TQuery, TDataContainerInstance, TField, TFieldValue, TDataContainerTemplate, TDataContainerSection>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, TDataContainerInstance?>
    where TQuery : GetDataContainerInstanceQuery<TDataContainerInstance>
    where TField : BaseDataContainerField, new()
    where TFieldValue : BaseDataContainerValue<TField>, new()
    where TDataContainerSection : BaseDataContainerSection<TField>, new()
    where TDataContainerTemplate : BaseDataContainer<TDataContainerSection, TField>, new()
    where TDataContainerInstance : BaseDataContainerInstance<TDataContainerTemplate, Guid, TDataContainerSection, TFieldValue, TField>, new()
{
    public async virtual Task<TDataContainerInstance?> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TDataContainerInstance>().SingleOrDefaultAsync(d => d.Id == query.Id, cancellationToken);
}

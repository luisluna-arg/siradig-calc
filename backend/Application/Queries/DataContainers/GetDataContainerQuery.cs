using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.DataContainers;

public abstract class GetDataContainerQuery<TDataContainerTemplate>(Guid id) : IRequest<TDataContainerTemplate?>
{
    [JsonIgnore]
    public Guid Id { get; set; } = id;
}

public abstract class GetDataContainerQueryHandler<TQuery, TDataContainerTemplate, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, TDataContainerTemplate?>
    where TQuery : GetDataContainerQuery<TDataContainerTemplate>
    where TField : BaseDataContainerField, new()
    where TDataContainerTemplate : BaseDataContainer<TField>, new()
{
    public async virtual Task<TDataContainerTemplate?> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TDataContainerTemplate>()
            .Include(d => d.Fields)
            .SingleOrDefaultAsync(d => d.Id == query.Id, cancellationToken);
}

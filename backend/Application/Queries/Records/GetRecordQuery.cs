using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Records;

public abstract class GetRecordQuery<TRecordTemplate>(Guid id) : IRequest<TRecordTemplate?>
{
    [JsonIgnore]
    public Guid Id { get; set; } = id;
}

public abstract class GetRecordQueryHandler<TQuery, TRecordTemplate, TRecordSection, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, TRecordTemplate?>
    where TQuery : GetRecordQuery<TRecordTemplate>
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TField : BaseRecordField, new()
{
    public async virtual Task<TRecordTemplate?> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TRecordTemplate>()
            .Include(d => d.Sections)
                .ThenInclude(d => d.Fields)
            .SingleOrDefaultAsync(d => d.Id == query.Id, cancellationToken);
}

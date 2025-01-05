using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Records;

public abstract class GetRecordInstanceQuery<TRecord>(Guid id) : IRequest<TRecord?>
{
    public Guid Id { get; set; } = id;
}

public abstract class GetRecordInstanceQueryHandler<TQuery, TRecord, TRecordId, TField, TValue, TRecordTemplate, TRecordSection>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, TRecord?>
    where TQuery : GetRecordInstanceQuery<TRecord>
    where TField : BaseRecordField, new()
    where TValue : BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
    where TRecord :  BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
{
    public async virtual Task<TRecord?> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TRecord>()
            .Include(i => i.Record)
                .ThenInclude(c => c.Sections)
                    .ThenInclude(s => s.Fields)
            .Include(i => i.Values)
                .ThenInclude(v => v.Field)
            .SingleOrDefaultAsync(i => i.Id == query.Id, cancellationToken);
}

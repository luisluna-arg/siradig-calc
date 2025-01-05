using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Records;

public abstract class GetRecordInstanceQuery<TRecord, TRecordId>(TRecordId id) : IRequest<TRecord?>
{
    public TRecordId Id { get; set; } = id;
}

public abstract class GetRecordInstanceQueryHandler<TQuery, TRecord, TRecordId, TField, TValue, TRecordTemplate, TRecordSection>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, TRecord?>
    where TQuery : GetRecordInstanceQuery<TRecord, TRecordId>
    where TField : BaseRecordField, new()
    where TValue : BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
    where TRecord :  BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
{
    public async virtual Task<TRecord?> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var record = await dbContext.Set<TRecord>()
            .AsNoTracking()
            .Include(i => i.RecordTemplate)
                .ThenInclude(c => c.Sections)
                    .ThenInclude(s => s.Fields)
            .SingleAsync(i => i.Id.Equals(query.Id), cancellationToken);
        
        /* TODO There's a cycle between record and values so EF can't solve it, this should be fixed */
        /* TODO Less important, find a way to compare Id's without 'Equals' */
        record.Values = await dbContext.Set<TValue>()
            .AsNoTracking()
            .Include(v => v.Field)
            .Where(v => v.RecordId!.Equals(query.Id))
            .ToListAsync(cancellationToken);

        return record;
    }
}

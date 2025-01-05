using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Records;

public abstract class GetRecordInstancesQuery<TRecordInstance> : IRequest<IEnumerable<TRecordInstance>>
{
}

public abstract class GetRecordInstancesQueryHandler<TQuery, TRecord, TRecordId, TField, TValue, TRecordTemplate, TRecordSection>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, IEnumerable<TRecord>>
    where TQuery : GetRecordInstancesQuery<TRecord>
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TField : BaseRecordField, new()
    where TRecord : BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
    where TValue : BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
{
    public async virtual Task<IEnumerable<TRecord>> Handle(TQuery query, CancellationToken cancellationToken)
    {
        var records = await dbContext.Set<TRecord>()
            .AsNoTracking()
            .Include(i => i.RecordTemplate)
                .ThenInclude(c => c.Sections)
                    .ThenInclude(s => s.Fields)
            .ToListAsync(cancellationToken);
        
        /* TODO There's a cycle between record and values so EF can't solve it, this should be fixed */
        /* TODO Less important, find a way to compare Id's without 'Equals' */
        var values = await dbContext.Set<TValue>()
            .AsNoTracking()
            .Include(v => v.Field)
            .ToListAsync(cancellationToken);

        foreach (var record in records)
        {
            record.Values = values.Where(v => v.RecordId!.Equals(record.Id)).ToList();
        }

        return records;
    }
}

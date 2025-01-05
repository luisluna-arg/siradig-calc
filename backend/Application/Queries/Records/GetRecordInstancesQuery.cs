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
        => await dbContext.Set<TRecord>()
            .Include(i => i.Record)
                .ThenInclude(c => c.Sections)
                    .ThenInclude(s => s.Fields)
            .Include(i => i.Values)
                .ThenInclude(v => v.Field)
            .ToArrayAsync();
}

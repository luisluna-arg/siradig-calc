using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Records;

public abstract class GetRecordInstancesQuery<TRecordInstance> : IRequest<IEnumerable<TRecordInstance>>
{
}

public abstract class GetRecordInstancesQueryHandler<TQuery, TRecordInstance, TField, TFieldValue, TRecordTemplate, TRecordSection>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, IEnumerable<TRecordInstance>>
    where TQuery : GetRecordInstancesQuery<TRecordInstance>
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TField : BaseRecordField, new()
    where TRecordInstance : BaseRecordInstance<TRecordTemplate, Guid, TRecordSection, TFieldValue, TField>, new()
    where TFieldValue : BaseRecordValue<TField>, new()
{
    public async virtual Task<IEnumerable<TRecordInstance>> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TRecordInstance>()
            .Include(i => i.Record)
                .ThenInclude(c => c.Sections)
                    .ThenInclude(s => s.Fields)
            .Include(i => i.Values)
                .ThenInclude(v => v.Field)
            .ToArrayAsync();
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Records;

public abstract class GetRecordInstanceQuery<TRecordInstance>(Guid id) : IRequest<TRecordInstance?>
{
    public Guid Id { get; set; } = id;
}

public abstract class GetRecordInstanceQueryHandler<TQuery, TRecordInstance, TField, TFieldValue, TRecordTemplate, TRecordSection>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, TRecordInstance?>
    where TQuery : GetRecordInstanceQuery<TRecordInstance>
    where TField : BaseRecordField, new()
    where TFieldValue : BaseRecordValue<TField>, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
    where TRecordInstance : BaseRecordInstance<TRecordTemplate, Guid, TRecordSection, TFieldValue, TField>, new()
{
    public async virtual Task<TRecordInstance?> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TRecordInstance>()
            .Include(i => i.Record)
                .ThenInclude(c => c.Sections)
                    .ThenInclude(s => s.Fields)
            .Include(i => i.Values)
                .ThenInclude(v => v.Field)
            .SingleOrDefaultAsync(i => i.Id == query.Id, cancellationToken);
}

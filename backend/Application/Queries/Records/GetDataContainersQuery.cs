using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Records;

public abstract class GetRecordsQuery<TRecordTemplate> : IRequest<IEnumerable<TRecordTemplate>>
{
}

public abstract class GetRecordsQueryHandler<TQuery, TRecordTemplate, TRecordSection, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<TQuery, IEnumerable<TRecordTemplate>>
    where TQuery : GetRecordsQuery<TRecordTemplate>
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TField : BaseRecordField, new()
{
    public async virtual Task<IEnumerable<TRecordTemplate>> Handle(TQuery query, CancellationToken cancellationToken)
        => await dbContext.Set<TRecordTemplate>()
            .Include(d => d.Sections)
                .ThenInclude(d => d.Fields)
            .ToArrayAsync();
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordQuery(Guid id) : IRequest<Record?>
{
    public Guid Id { get; set; } = id;
}

public class GetRecordQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<GetRecordQuery, Record?>
{
    public async virtual Task<Record?> Handle(GetRecordQuery query, CancellationToken cancellationToken)
    {
        var record = await dbContext.Records
            .AsNoTracking()
            .Include(i => i.Template)
                .ThenInclude(c => c.Sections)
                    .ThenInclude(s => s.Fields)
            .SingleAsync(i => i.Id.Equals(query.Id), cancellationToken);
        
        /* TODO There's a cycle between record and values so EF can't solve it, this should be fixed */
        /* TODO Less important, find a way to compare Id's without 'Equals' */
        record.Values = await dbContext.RecordValues
            .AsNoTracking()
            .Include(v => v.Field)
            .Where(v => v.RecordId!.Equals(query.Id))
            .ToListAsync(cancellationToken);

        return record;
    }
}

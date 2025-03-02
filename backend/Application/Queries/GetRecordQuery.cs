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
                .ThenInclude(c => c.Sections.OrderBy(s => s.Name))
                    .ThenInclude(s => s.Fields.OrderBy(s => s.Label))
            .SingleAsync(i => i.Id.Equals(query.Id), cancellationToken);
        
        record.Values = await dbContext.RecordValues
            .AsNoTracking()
            .Include(v => v.Field)
            .OrderBy(s => s.Field.Label)
            .Where(v => v.RecordId == query.Id)
            .ToListAsync(cancellationToken);

        return record;
    }
}

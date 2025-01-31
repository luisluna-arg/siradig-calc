using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordInstancesQuery : IRequest<IEnumerable<Record>>
{
}

public class GetRecordInstancesQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<GetRecordInstancesQuery, IEnumerable<Record>>
{
    public async virtual Task<IEnumerable<Record>> Handle(GetRecordInstancesQuery query, CancellationToken cancellationToken)
    {
        var records = await dbContext.Set<Record>()
            .AsNoTracking()
            .Include(i => i.Template)
                .ThenInclude(c => c.Sections)
                    .ThenInclude(s => s.Fields)
            .ToListAsync(cancellationToken);
        
        /* TODO There's a cycle between record and values so EF can't solve it, this should be fixed */
        /* TODO Less important, find a way to compare Id's without 'Equals' */
        var values = await dbContext.RecordValues
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

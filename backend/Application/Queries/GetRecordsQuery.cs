using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordsQuery(Guid? templateId) : IRequest<IEnumerable<Record>>
{
    [JsonIgnore]
    public Guid? TemplateId { get; set; } = templateId;
}

public class GetRecordsQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<GetRecordsQuery, IEnumerable<Record>>
{
    public async virtual Task<IEnumerable<Record>> Handle(GetRecordsQuery query, CancellationToken cancellationToken)
    {
        var recordsQuery = dbContext.Records
            .AsNoTracking()
            .Include(i => i.Template)
                .ThenInclude(c => c.Sections.OrderBy(s => s.Name))
                    .ThenInclude(s => s.Fields.OrderBy(s => s.Label))
            .OrderBy(r => r.Template.Name)
            .ThenBy(r => r.Title)
            .AsQueryable();

        if (query.TemplateId.HasValue)
        {
            recordsQuery = recordsQuery.Where(r => r.TemplateId == query.TemplateId);
        }

        var records = await recordsQuery.ToListAsync(cancellationToken);

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

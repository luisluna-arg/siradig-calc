using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordTemplateQuery(Guid id) : IRequest<RecordTemplate?>
{
    [JsonIgnore]
    public Guid Id { get; set; } = id;
}

public class GetRecordTemplateQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<GetRecordTemplateQuery, RecordTemplate?>
{
    public async virtual Task<RecordTemplate?> Handle(GetRecordTemplateQuery query, CancellationToken cancellationToken)
        => await dbContext.RecordTemplates
            .Include(d => d.Sections)
                .ThenInclude(d => d.Fields)
            .SingleOrDefaultAsync(d => d.Id == query.Id, cancellationToken);
}
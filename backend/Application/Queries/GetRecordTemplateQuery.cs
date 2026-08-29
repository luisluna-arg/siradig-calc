using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordTemplateQuery(Guid id) : IRequest<RecordTemplateDto?>
{
    [JsonIgnore]
    public Guid Id { get; set; } = id;
}

public class GetRecordTemplateQueryHandler(ISolutionDbContext dbContext, IDtoMappingService mapperManager)
    : IRequestHandler<GetRecordTemplateQuery, RecordTemplateDto?>
{
    public async virtual Task<RecordTemplateDto?> Handle(GetRecordTemplateQuery query, CancellationToken cancellationToken)
    {
        var recordTemplate = await dbContext.RecordTemplates
            .Include(d => d.Sections)
                .ThenInclude(d => d.Fields)
            .SingleOrDefaultAsync(d => d.Id == query.Id, cancellationToken);

        return recordTemplate == null ? null : mapperManager.Map<RecordTemplateDto>(recordTemplate);
    }
}
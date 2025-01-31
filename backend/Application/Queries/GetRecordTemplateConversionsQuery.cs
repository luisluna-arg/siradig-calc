using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordTemplateConversionsQuery(Guid sourceRecordId)
    : IRequest<ICollection<RecordTemplateConversionDto>>
{
    [JsonIgnore]
    public Guid SourceRecordId { get; set; } = sourceRecordId;
}

public class GetRecordTemplateConversionsQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : IRequestHandler<GetRecordTemplateConversionsQuery, ICollection<RecordTemplateConversionDto>>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IDtoMappingService _dtoMappingService = dtoMappingService;

    public async Task<ICollection<RecordTemplateConversionDto>> Handle(GetRecordTemplateConversionsQuery request, CancellationToken cancellationToken)
    {
        var conversions = await _dbContext.RecordTemplateConversions
            .AsNoTracking()
            .Include(c => c.Source)
                .ThenInclude(l => l.Template)
                    .ThenInclude(t => t.Sections)
                        .ThenInclude(t => t.Fields)
            .Include(c => c.Source)
                .ThenInclude(l => l.Values)

            .Include(c => c.Target)
                .ThenInclude(l => l.Template)
                    .ThenInclude(t => t.Sections)
                        .ThenInclude(t => t.Fields)
            .Include(c => c.Target)
                .ThenInclude(l => l.Values)
            .Where(r => 
                r.SourceId == request.SourceRecordId)
            .ToArrayAsync(cancellationToken);

        var templateLinks = await _dbContext.RecordTemplateConversions
            .AsNoTracking()
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(c => c.RecordFieldLinks)
                    .ThenInclude(c => c.RightField)
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(c => c.RecordFieldLinks)
                    .ThenInclude(c => c.LeftField)
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(l => l.LeftTemplate)
                    .ThenInclude(t => t.Sections)
            .Where(r => 
                r.SourceId == request.SourceRecordId)
            .ToArrayAsync(cancellationToken);

        foreach (var conversion in conversions)
        {
            conversion.RecordTemplateLink = templateLinks.FirstOrDefault(l => l.Id == conversion.Id)?.RecordTemplateLink!;
        }

        return _dtoMappingService.Map<RecordTemplateConversionDto>(conversions);
    }
}

using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordTemplateConversionsQuery(Guid? sourceRecordId)
    : IRequest<ICollection<RecordTemplateConversionDto>>
{
    [JsonIgnore]
    public Guid? SourceRecordId { get; set; } = sourceRecordId;
}

public class GetRecordTemplateConversionsQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : IRequestHandler<GetRecordTemplateConversionsQuery, ICollection<RecordTemplateConversionDto>>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IDtoMappingService _dtoMappingService = dtoMappingService;

    public async Task<ICollection<RecordTemplateConversionDto>> Handle(GetRecordTemplateConversionsQuery request, CancellationToken cancellationToken)
    {
        var conversionQueries = _dbContext.RecordConversions
            .AsNoTracking()
            .Include(c => c.Source)
                .ThenInclude(l => l.Template)
                    .ThenInclude(t => t.Sections)
                        .ThenInclude(t => t.Fields)
            .Include(c => c.Source)
                .ThenInclude(l => l.Values)
                    .ThenInclude(l => l.Field)

            .Include(c => c.Target)
                .ThenInclude(l => l.Template)
                    .ThenInclude(t => t.Sections)
                        .ThenInclude(t => t.Fields)
            .Include(c => c.Target)
                .ThenInclude(l => l.Values)
                    .ThenInclude(l => l.Field)
            .OrderBy(c => c.Source.Title)
            .ThenBy(c => c.Source.Template.Name)
            .AsQueryable();

        var templateLinksQuery = _dbContext.RecordConversions
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
            .OrderBy(c => c.Source.Title)
            .ThenBy(c => c.Source.Template.Name)
            .AsQueryable();

        if (request.SourceRecordId.HasValue)
        {
            conversionQueries = conversionQueries.Where(r => r.SourceId == request.SourceRecordId);
            templateLinksQuery = templateLinksQuery.Where(r => r.SourceId == request.SourceRecordId);
        }

        var templateLinks = await templateLinksQuery.ToArrayAsync(cancellationToken);
        var conversions = await conversionQueries.ToArrayAsync(cancellationToken);

        foreach (var conversion in conversions)
        {
            conversion.RecordTemplateLink = templateLinks.FirstOrDefault(l => l.Id == conversion.Id)?.RecordTemplateLink!;
        }

        return _dtoMappingService.Map<RecordTemplateConversionDto>(conversions);
    }
}

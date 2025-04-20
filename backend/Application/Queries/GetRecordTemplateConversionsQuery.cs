using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordTemplateConversionsQuery(Guid? sourceRecordId)
    : IRequest<ICollection<RecordTemplateConversionCompactDto>>
{
    [JsonIgnore]
    public Guid? SourceRecordId { get; set; } = sourceRecordId;
}

public class GetRecordTemplateConversionsQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : IRequestHandler<GetRecordTemplateConversionsQuery, ICollection<RecordTemplateConversionCompactDto>>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IDtoMappingService _dtoMappingService = dtoMappingService;

    public async Task<ICollection<RecordTemplateConversionCompactDto>> Handle(GetRecordTemplateConversionsQuery request, CancellationToken cancellationToken)
    {
        var conversionQueries = _dbContext.RecordConversions
            .AsNoTracking()
            .Include(c => c.Source)
                .ThenInclude(l => l.Template)
            .Include(c => c.Source)
            .Include(c => c.Target)
                .ThenInclude(l => l.Template)
            .Include(c => c.Target)
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
            .OrderBy(c => c.Source.Title)
            .ThenBy(c => c.Source.Template.Name)
            .AsQueryable();

        if (request.SourceRecordId.HasValue)
        {
            conversionQueries = conversionQueries.Where(r => r.SourceId == request.SourceRecordId);
            templateLinksQuery = templateLinksQuery.Where(r => r.SourceId == request.SourceRecordId);
        }

        var templateLinks = await templateLinksQuery.AsSplitQuery().ToArrayAsync(cancellationToken);
        var conversions = await conversionQueries.AsSplitQuery().ToArrayAsync(cancellationToken);

        foreach (var conversion in conversions)
        {
            conversion.RecordTemplateLink = templateLinks.FirstOrDefault(l => l.Id == conversion.Id)?.RecordTemplateLink!;
        }

        return _dtoMappingService.Map<RecordTemplateConversionCompactDto>(conversions);
    }
}

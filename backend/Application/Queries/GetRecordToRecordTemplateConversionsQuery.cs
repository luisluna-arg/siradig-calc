using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordToRecordTemplateConversionsQuery(Guid sourceRecordId, Guid targetRecordId)
    : IRequest<ICollection<RecordTemplateConversionDto>>
{
    [JsonIgnore]
    public Guid SourceRecordId { get; set; } = sourceRecordId;

    [JsonIgnore]
    public Guid TargetRecordId { get; set; } = targetRecordId;
}

public class GetRecordToTemplateConversionsQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : IRequestHandler<GetRecordToRecordTemplateConversionsQuery, ICollection<RecordTemplateConversionDto>>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IDtoMappingService _dtoMappingService = dtoMappingService;

    public async Task<ICollection<RecordTemplateConversionDto>> Handle(GetRecordToRecordTemplateConversionsQuery request, CancellationToken cancellationToken)
    {
        var recordConversions = await _dbContext.RecordConversions
            .AsNoTracking()
            .Include(c => c.Source)
                .ThenInclude(s => s.Template)
                    .ThenInclude(s => s.Sections)
                        .ThenInclude(s => s.Fields)
            .Include(c => c.Source)
                .ThenInclude(s => s.Values)
            .Include(c => c.Target)
                .ThenInclude(s => s.Template)
                    .ThenInclude(s => s.Sections)
            .Include(c => c.Target)
                .ThenInclude(c => c.Values)
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(c => c.RecordFieldLinks)
                    .ThenInclude(c => c.LeftField)
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(c => c.RecordFieldLinks)
                    .ThenInclude(c => c.RightField)
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(c => c.LeftTemplate)
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(c => c.RightTemplate)
            .Where(r => 
                r.SourceId == request.SourceRecordId &&
                r.TargetId == request.TargetRecordId)
            .ToArrayAsync(cancellationToken);

        return _dtoMappingService.Map<RecordTemplateConversionDto>(recordConversions);
    }
}

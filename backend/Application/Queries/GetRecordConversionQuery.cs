using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordConversionQuery(Guid recordConversionId)
    : IRequest<RecordTemplateConversionDto>
{
    [JsonIgnore]
    public Guid RecordConversionId { get; set; } = recordConversionId;
}

public class GetRecordConversionQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : IRequestHandler<GetRecordConversionQuery, RecordTemplateConversionDto>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IDtoMappingService _dtoMappingService = dtoMappingService;

    public async Task<RecordTemplateConversionDto> Handle(GetRecordConversionQuery request, CancellationToken cancellationToken)
    {
        var recordConversion = await _dbContext.RecordConversions
            .AsNoTracking()
            .Include(c => c.Source)
                .ThenInclude(s => s.Template)
                    .ThenInclude(s => s.Sections)
                        .ThenInclude(s => s.Fields)
            .Include(c => c.Source)
                .ThenInclude(s => s.Values)
                    .ThenInclude(s => s.Field)
            .Include(c => c.Target)
                .ThenInclude(s => s.Template)
                    .ThenInclude(s => s.Sections)
            .Include(c => c.Target)
                .ThenInclude(c => c.Values)
                    .ThenInclude(c => c.Field)
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
            .FirstAsync(r => r.Id == request.RecordConversionId, cancellationToken);

        return _dtoMappingService.Map<RecordTemplateConversionDto>(recordConversion);
    }
}

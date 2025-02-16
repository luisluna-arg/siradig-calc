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
            .Include(c => c.Source)
            .Include(c => c.Target)
            .Where(r => 
                r.SourceId == request.SourceRecordId &&
                r.TargetId == request.TargetRecordId)
            .ToArrayAsync(cancellationToken);

        return _dtoMappingService.Map<RecordTemplateConversionDto>(recordConversions);
    }
}

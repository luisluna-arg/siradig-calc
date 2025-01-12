using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptToFormConversionsQuery(Guid sourceRecordId, Guid targetRecordId)
    : IRequest<ICollection<ReceiptToFormConversionDto>>
{
    [JsonIgnore]
    public Guid SourceRecordId { get; set; } = sourceRecordId;

    [JsonIgnore]
    public Guid TargetRecordId { get; set; } = targetRecordId;
}

public class GetReceiptToFormConversionsQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : IRequestHandler<GetReceiptToFormConversionsQuery, ICollection<ReceiptToFormConversionDto>>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IDtoMappingService _dtoMappingService = dtoMappingService;

    public async Task<ICollection<ReceiptToFormConversionDto>> Handle(GetReceiptToFormConversionsQuery request, CancellationToken cancellationToken)
    {
        var recordConversions = await _dbContext.ReceiptToFormConversions
            .Include(c => c.Source)
            .Include(c => c.Target)
            .Where(r => 
                r.SourceId == request.SourceRecordId &&
                r.TargetId == request.TargetRecordId)
            .ToArrayAsync(cancellationToken);

        return _dtoMappingService.Map<ReceiptToFormConversionDto>(recordConversions);
    }
}

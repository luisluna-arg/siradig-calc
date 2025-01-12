using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Dtos.Conversion.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptConversionsQuery(Guid sourceRecordId)
    : IRequest<ICollection<ReceiptToFormConversionDto>>
{
    [JsonIgnore]
    public Guid SourceRecordId { get; set; } = sourceRecordId;
}

public class GetReceiptConversionsQueryHandler(ISolutionDbContext dbContext, IDtoMappingService dtoMappingService)
    : IRequestHandler<GetReceiptConversionsQuery, ICollection<ReceiptToFormConversionDto>>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IDtoMappingService _dtoMappingService = dtoMappingService;

    public async Task<ICollection<ReceiptToFormConversionDto>> Handle(GetReceiptConversionsQuery request, CancellationToken cancellationToken)
    {
        var conversions = await _dbContext.ReceiptToFormConversions
            .AsNoTracking()
            .Include(c => c.Source)
                .ThenInclude(l => l.RecordTemplate)
                    .ThenInclude(t => t.Sections)
                        .ThenInclude(t => t.Fields)
            .Include(c => c.Source)
                .ThenInclude(l => l.Values)

            .Include(c => c.Target)
                .ThenInclude(l => l.RecordTemplate)
                    .ThenInclude(t => t.Sections)
                        .ThenInclude(t => t.Fields)
            .Include(c => c.Target)
                .ThenInclude(l => l.Values)
            .Where(r => 
                r.SourceId == request.SourceRecordId)
            .ToArrayAsync(cancellationToken);

        var templateLinks = await _dbContext.ReceiptToFormConversions
            .AsNoTracking()
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(c => c.RecordFieldLinks)
                    .ThenInclude(c => c.FormField)
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(c => c.RecordFieldLinks)
                    .ThenInclude(c => c.ReceiptField)
            .Include(c => c.RecordTemplateLink)
                .ThenInclude(l => l.ReceiptTemplate)
                    .ThenInclude(t => t.Sections)
            .Where(r => 
                r.SourceId == request.SourceRecordId)
            .ToArrayAsync(cancellationToken);

        foreach (var conversion in conversions)
        {
            conversion.RecordTemplateLink = templateLinks.FirstOrDefault(l => l.Id == conversion.Id)?.RecordTemplateLink!;
        }

        return _dtoMappingService.Map<ReceiptToFormConversionDto>(conversions);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class ReceiptLinksQuery(Guid receiptTemplateId) : IRequest<IEnumerable<RecordTemplateLink>>
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
}

public class ReceiptLinksQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<ReceiptLinksQuery, IEnumerable<RecordTemplateLink>>
{
    public async Task<IEnumerable<RecordTemplateLink>> Handle(ReceiptLinksQuery request, CancellationToken cancellationToken)
        => await dbContext.RecordTemplateLinks
            .Include(l => l.FormTemplate)
                .ThenInclude(l => l.Sections)
                    .ThenInclude(l => l.Fields)
            .Include(l => l.ReceiptTemplate)
                .ThenInclude(l => l.Sections)
                    .ThenInclude(l => l.Fields)
            .Where(l => l.ReceiptTemplateId == request.ReceiptTemplateId)
            .ToListAsync(cancellationToken);
}

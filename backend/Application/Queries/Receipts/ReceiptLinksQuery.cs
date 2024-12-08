using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class ReceiptLinksQuery(Guid receiptTemplateId) : IRequest<IEnumerable<DataContainerLink>>
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
}

public class ReceiptLinksQueryHandler(ISolutionDbContext context)
    : IRequestHandler<ReceiptLinksQuery, IEnumerable<DataContainerLink>>
{
    public async Task<IEnumerable<DataContainerLink>> Handle(ReceiptLinksQuery request, CancellationToken cancellationToken)
        => await context.DataContainerLinks
            .Include(l => l.FormTemplate)
                .ThenInclude(l => l.Fields)
            .Include(l => l.ReceiptTemplate)
                .ThenInclude(l => l.Fields)
            .Where(l => l.ReceiptTemplateId == request.ReceiptTemplateId)
            .ToListAsync(cancellationToken);
}

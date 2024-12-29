using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class LinkTemplatesQuery(Guid receiptTemplateId, Guid formTemplateId) : IRequest<RecordTemplateLink?>
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
    public Guid FormTemplateId { get; } = formTemplateId;
}

public class LinkTemplatesQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<LinkTemplatesQuery, RecordTemplateLink?>
{
    public async Task<RecordTemplateLink?> Handle(LinkTemplatesQuery request, CancellationToken cancellationToken)
        => await dbContext.RecordTemplateLinks
            .SingleOrDefaultAsync(l => l.FormTemplateId == request.FormTemplateId && l.ReceiptTemplateId == request.ReceiptTemplateId);
}

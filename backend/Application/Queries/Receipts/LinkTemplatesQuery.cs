using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class LinkTemplatesQuery(Guid receiptTemplateId, Guid formTemplateId) : IRequest<DataContainerLink?>
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
    public Guid FormTemplateId { get; } = formTemplateId;
}

public class LinkTemplatesQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<LinkTemplatesQuery, DataContainerLink?>
{
    public async Task<DataContainerLink?> Handle(LinkTemplatesQuery request, CancellationToken cancellationToken)
        => await dbContext.DataContainerLinks
            .SingleOrDefaultAsync(l => l.FormTemplateId == request.FormTemplateId && l.ReceiptTemplateId == request.ReceiptTemplateId);
}

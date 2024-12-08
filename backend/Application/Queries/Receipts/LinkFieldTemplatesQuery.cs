using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class LinkFieldTemplatesQuery(Guid receiptTemplateId, Guid formTemplateId, Guid receiptFieldId, Guid formFieldId) : IRequest<DataContainerFieldLink?>
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
    public Guid FormTemplateId { get; } = formTemplateId;
    public Guid ReceiptFieldId { get; } = receiptFieldId;
    public Guid FormFieldId { get; } = formFieldId;
}

public class LinkFieldTemplatesQueryHandler(ISolutionDbContext context)
    : IRequestHandler<LinkFieldTemplatesQuery, DataContainerFieldLink?>
{
    public async Task<DataContainerFieldLink?> Handle(LinkFieldTemplatesQuery request, CancellationToken cancellationToken)
        => await context.DataContainerFieldLinks
            .Include(f => f.FormField)
            .Include(f => f.ReceiptField)
            .SingleOrDefaultAsync(l => 
                l.FormFieldId == request.FormFieldId &&
                l.ReceiptFieldId == request.ReceiptTemplateId);
}

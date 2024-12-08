using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class LinkFieldTemplatesCommand(Guid receiptTemplateId, Guid formTemplateId, Guid receiptFieldId, Guid formFieldId) : IRequest<Guid>
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
    public Guid FormTemplateId { get; } = formTemplateId;
    public Guid ReceiptFieldId { get; } = receiptFieldId;
    public Guid FormFieldId { get; } = formFieldId;
}

public class LinkFieldTemplatesCommandHandler(ISolutionDbContext context)
    : IRequestHandler<LinkFieldTemplatesCommand, Guid>
{
    public async Task<Guid> Handle(LinkFieldTemplatesCommand request, CancellationToken cancellationToken)
    {
        var link = await context.DataContainerLinks
            .Include(f => f.FormTemplate)
                .ThenInclude(d => d.Fields)
            .Include(f => f.ReceiptTemplate)
                .ThenInclude(d => d.Fields)
            .SingleOrDefaultAsync(l => 
                l.FormTemplateId == request.FormTemplateId && 
                l.ReceiptTemplateId == request.ReceiptTemplateId &&
                l.FormTemplate.Fields.Any(f => f.Id == request.FormFieldId) &&
                l.ReceiptTemplate.Fields.Any(f => f.Id == request.ReceiptTemplateId));

        if (link != null)
        {
            return link.Id;
        }

        var newLink = new DataContainerFieldLink()
        {
            Id = Guid.NewGuid(),
            FormFieldId = request.FormFieldId,
            ReceiptFieldId = request.ReceiptFieldId,
        };

        await context.DataContainerFieldLinks.AddAsync(newLink, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newLink.Id;
    }
}

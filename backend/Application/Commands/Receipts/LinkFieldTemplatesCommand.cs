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

public class LinkFieldTemplatesCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<LinkFieldTemplatesCommand, Guid>
{
    public async Task<Guid> Handle(LinkFieldTemplatesCommand request, CancellationToken cancellationToken)
    {
        var link = await dbContext.RecordTemplateLinks
            .Include(f => f.FormTemplate)
                .ThenInclude(d => d.Sections)
                    .ThenInclude(d => d.Fields)
            .Include(f => f.ReceiptTemplate)
                .ThenInclude(d => d.Sections)
                    .ThenInclude(d => d.Fields)
            .SingleOrDefaultAsync(l =>
                l.FormTemplateId == request.FormTemplateId &&
                l.ReceiptTemplateId == request.ReceiptTemplateId &&
                l.FormTemplate.Sections.Any(s => s.Fields.Any(f => f.Id == request.FormFieldId)) &&
                l.ReceiptTemplate.Sections.Any(s => s.Fields.Any(f => f.Id == request.ReceiptTemplateId)));

        if (link != null)
        {
            return link.Id;
        }

        var newLink = new RecordFieldLink()
        {
            Id = Guid.NewGuid(),
            FormFieldId = request.FormFieldId,
            ReceiptFieldId = request.ReceiptFieldId,
        };

        await dbContext.RecordFieldLinks.AddAsync(newLink, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return newLink.Id;
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class LinkTemplatesCommand(Guid receiptTemplateId, Guid formTemplateId) : IRequest<Guid>
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
    public Guid FormTemplateId { get; } = formTemplateId;
}

public class LinkTemplatesCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<LinkTemplatesCommand, Guid>
{
    public async Task<Guid> Handle(LinkTemplatesCommand request, CancellationToken cancellationToken)
    {
        var link = await dbContext.RecordTemplateLinks
            .SingleOrDefaultAsync(l => l.FormTemplateId == request.FormTemplateId && l.ReceiptTemplateId == request.ReceiptTemplateId);

        if (link != null)
        {
            return link.Id;
        }

        var newLink = new RecordTemplateLink()
        {
            Id = Guid.NewGuid(),
            FormTemplateId = request.FormTemplateId,
            ReceiptTemplateId = request.ReceiptTemplateId
        };

        await dbContext.RecordTemplateLinks.AddAsync(newLink, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return newLink.Id;
    }
}

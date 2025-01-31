using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class LinkTemplatesCommand(Guid leftTemplateId, Guid rightTemplateId) : IRequest<Guid>
{
    public Guid LeftTemplateId { get; } = leftTemplateId;
    public Guid RightTemplateId { get; } = rightTemplateId;
}

public class LinkTemplatesCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<LinkTemplatesCommand, Guid>
{
    public async Task<Guid> Handle(LinkTemplatesCommand request, CancellationToken cancellationToken)
    {
        var link = await dbContext.RecordTemplateLinks
            .SingleOrDefaultAsync(l => l.RightTemplateId == request.RightTemplateId && l.LeftTemplateId == request.LeftTemplateId);

        if (link != null)
        {
            return link.Id;
        }

        var newLink = new RecordTemplateLink()
        {
            Id = Guid.NewGuid(),
            LeftTemplateId = request.LeftTemplateId,
            RightTemplateId = request.RightTemplateId
        };

        await dbContext.RecordTemplateLinks.AddAsync(newLink, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return newLink.Id;
    }
}

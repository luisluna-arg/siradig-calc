using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class LinkFieldTemplatesCommand(Guid leftTemplateId, Guid rightTemplateId, Guid leftFieldId, Guid rightFieldId) : IRequest<Guid>
{
    public Guid LeftTemplateId { get; } = leftTemplateId;
    public Guid RightTemplateId { get; } = rightTemplateId;
    public Guid RightFieldId { get; } = rightFieldId;
    public Guid LeftFieldId { get; } = leftFieldId;
}

public class LinkFieldTemplatesCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<LinkFieldTemplatesCommand, Guid>
{
    public async Task<Guid> Handle(LinkFieldTemplatesCommand request, CancellationToken cancellationToken)
    {
        var templateLink = await dbContext.RecordTemplateLinks
            .Include(f => f.RightTemplate)
                .ThenInclude(d => d.Sections)
                    .ThenInclude(d => d.Fields)
            .Include(f => f.LeftTemplate)
                .ThenInclude(d => d.Sections)
                    .ThenInclude(d => d.Fields)
            .SingleAsync(l =>
                l.RightTemplateId == request.RightTemplateId &&
                l.LeftTemplateId == request.LeftTemplateId &&
                l.RightTemplate.Sections.Any(s => s.Fields.Any(f => f.Id == request.RightFieldId)) &&
                l.LeftTemplate.Sections.Any(s => s.Fields.Any(f => f.Id == request.LeftFieldId)));

        var newLink = new RecordTemplateFieldLink()
        {
            TemplateLink = templateLink,
            RightFieldId = request.RightFieldId,
            LeftFieldId = request.LeftFieldId,
        };

        await dbContext.RecordFieldLinks.AddAsync(newLink, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return newLink.Id;
    }
}

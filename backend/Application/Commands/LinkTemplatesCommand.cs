using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class LinkTemplatesCommand() : IRequest<Guid>
{
    [JsonIgnore]
    public Guid LeftTemplateId { get; set; }
    [JsonIgnore]
    public Guid RightTemplateId { get; set; }
    public List<CreateFieldLinkDTO> FieldLinks { get; set; } = [];
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
            LeftTemplateId = request.LeftTemplateId,
            RightTemplateId = request.RightTemplateId,
            RecordFieldLinks = request.FieldLinks
                .Select(l => new RecordTemplateFieldLink() { LeftFieldId = l.LeftFieldId, RightFieldId = l.RightFieldId })
                .ToList()
        };

        await dbContext.RecordTemplateLinks.AddAsync(newLink, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return newLink.Id;
    }
}

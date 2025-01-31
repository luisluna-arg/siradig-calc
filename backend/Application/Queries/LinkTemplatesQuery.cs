using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class LinkTemplatesQuery(Guid leftTemplateId, Guid rightTemplateId) : IRequest<RecordTemplateLinkDto?>
{
    public Guid LeftTemplateId { get; } = leftTemplateId;
    public Guid RightTemplateId { get; } = rightTemplateId;
}

public class LinkTemplatesQueryHandler(ISolutionDbContext dbContext, IDtoMappingService mapperManager)
    : IRequestHandler<LinkTemplatesQuery, RecordTemplateLinkDto?>
{
    public async Task<RecordTemplateLinkDto?> Handle(LinkTemplatesQuery request, CancellationToken cancellationToken)
    {
        /* TODO There's a cycle between RecordTemplateLinks and Fields so EF can't solve it, this should be fixed */
        var templateLink = await dbContext.RecordTemplateLinks
            .AsNoTracking()
            .Include(l => l.RightTemplate)
            .Include(l => l.LeftTemplate)
            .FirstAsync(l => l.LeftTemplateId == request.LeftTemplateId &&
            l.RightTemplateId == request.RightTemplateId, cancellationToken);

        templateLink.RecordFieldLinks = await dbContext.RecordFieldLinks
            .AsNoTracking()
            .Include(l => l.RightField)
            .Include(l => l.LeftField)
            .Where(l => l.TemplateLinkId == templateLink.Id)
            .ToListAsync(cancellationToken);

        return mapperManager.Map<RecordTemplateLinkDto>(templateLink);
    }
}

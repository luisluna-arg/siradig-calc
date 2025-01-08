using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class LinkTemplatesQuery(Guid receiptTemplateId, Guid formTemplateId) : IRequest<RecordTemplateLinkDto?>
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
    public Guid FormTemplateId { get; } = formTemplateId;
}

public class LinkTemplatesQueryHandler(ISolutionDbContext dbContext, IDtoMappingService mapperManager)
    : IRequestHandler<LinkTemplatesQuery, RecordTemplateLinkDto?>
{
    public async Task<RecordTemplateLinkDto?> Handle(LinkTemplatesQuery request, CancellationToken cancellationToken)
    {
        /* TODO There's a cycle between RecordTemplateLinks and Fields so EF can't solve it, this should be fixed */
        var templateLink = await dbContext.RecordTemplateLinks
            .AsNoTracking()
            .Include(l => l.FormTemplate)
            .Include(l => l.ReceiptTemplate)
            .FirstAsync(l => l.ReceiptTemplateId == request.ReceiptTemplateId &&
            l.FormTemplateId == request.FormTemplateId, cancellationToken);

        templateLink.RecordFieldLinks = await dbContext.RecordFieldLinks
            .AsNoTracking()
            .Include(l => l.FormField)
            .Include(l => l.ReceiptField)
            .Where(l => l.TemplateLinkId == templateLink.Id)
            .ToListAsync(cancellationToken);

        return mapperManager.Map<RecordTemplateLinkDto>(templateLink);
    }
}

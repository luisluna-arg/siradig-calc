using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordTemplateLinksQuery() : IRequest<ICollection<RecordTemplateLinkDto>>
{
}

public class GetRecordTemplateLinksQueryHandler(ISolutionDbContext dbContext, IDtoMappingService mapperManager)
    : IRequestHandler<GetRecordTemplateLinksQuery, ICollection<RecordTemplateLinkDto>>
{
    public async Task<ICollection<RecordTemplateLinkDto>> Handle(GetRecordTemplateLinksQuery request, CancellationToken cancellationToken)
    {
        /* TODO There's a cycle between RecordTemplateLinks and Fields so EF can't solve it, this should be fixed */
        var templateLinks = await dbContext.RecordTemplateLinks
            .AsNoTracking()
            .Include(l => l.RightTemplate)
            .Include(l => l.LeftTemplate)
            .ToListAsync(cancellationToken);

        var recordFieldLinkDictionary = await dbContext.RecordFieldLinks
            .AsNoTracking()
            .Include(l => l.RightField)
            .Include(l => l.LeftField)
            .GroupBy(k => k.TemplateLinkId)
            .ToDictionaryAsync(k => k.Key, v => v.ToList(), cancellationToken);

        foreach (var templateLink in templateLinks)
        {
            List<RecordTemplateFieldLink>? recordFieldLinks;
            if (recordFieldLinkDictionary.TryGetValue(templateLink.Id, out recordFieldLinks))
            {
                templateLink.RecordFieldLinks = recordFieldLinks;
            }
        }

        return mapperManager.Map<RecordTemplateLinkDto>(templateLinks);
    }
}

using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordTemplateLinkQuery(Guid leftTemplateId, Guid rightTemplateId) : IRequest<RecordTemplateLinkDto>
{
    [JsonIgnore]
    public Guid LeftTemplateId { get; } = leftTemplateId;
    [JsonIgnore]
    public Guid RightTemplateId { get; } = rightTemplateId;
}

public class GetRecordTemplateLinkQueryHandler(ISolutionDbContext dbContext, IDtoMappingService mapperManager)
    : IRequestHandler<GetRecordTemplateLinkQuery, RecordTemplateLinkDto>
{
    public async Task<RecordTemplateLinkDto> Handle(GetRecordTemplateLinkQuery request, CancellationToken cancellationToken)
    {
        /* TODO There's a cycle between RecordTemplateLinks and Fields so EF can't solve it, this should be fixed */
        var templateLink = await dbContext.RecordTemplateLinks
            .AsNoTracking()
            .Include(l => l.RightTemplate)
            .Include(l => l.LeftTemplate)
            .FirstAsync(l => l.LeftTemplateId == request.LeftTemplateId &&
                l.LeftTemplateId == request.LeftTemplateId, cancellationToken);

        templateLink.RecordFieldLinks = await dbContext.RecordFieldLinks
            .AsNoTracking()
            .Include(l => l.RightField)
            .Include(l => l.LeftField)
            .Where(l => l.TemplateLinkId == templateLink.Id)
            .ToListAsync(cancellationToken);

        return mapperManager.Map<RecordTemplateLinkDto>(templateLink);
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordTemplatesQuery : IRequest<IEnumerable<RecordTemplateDto>>
{
}

public class GetRecordTemplatesQueryHandler(ISolutionDbContext dbContext, IDtoMappingService mapperManager)
    : IRequestHandler<GetRecordTemplatesQuery, IEnumerable<RecordTemplateDto>>
{
    public async virtual Task<IEnumerable<RecordTemplateDto>> Handle(GetRecordTemplatesQuery query, CancellationToken cancellationToken)
    {
        var recordTemplates = await dbContext.RecordTemplates
            .AsNoTracking()
            .Include(d => d.Sections.OrderBy(s => s.Name))
                .ThenInclude(d => d.Fields.OrderBy(s => s.Label))
            .OrderBy(t => t.Name)
            .ToArrayAsync(cancellationToken);

        return mapperManager.Map<RecordTemplateDto>(recordTemplates);
    }
}

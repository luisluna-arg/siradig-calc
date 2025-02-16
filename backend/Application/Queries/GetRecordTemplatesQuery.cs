using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries;

public class GetRecordTemplatesQuery : IRequest<IEnumerable<RecordTemplate>>
{
}

public class GetRecordTemplatesQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<GetRecordTemplatesQuery, IEnumerable<RecordTemplate>>
{
    public async virtual Task<IEnumerable<RecordTemplate>> Handle(GetRecordTemplatesQuery query, CancellationToken cancellationToken)
        => await dbContext.RecordTemplates
            .AsNoTracking()
            .Include(d => d.Sections.OrderBy(s => s.Name))
                .ThenInclude(d => d.Fields.OrderBy(s => s.Label))
            .OrderBy(t => t.Name)
            .ToArrayAsync();
}

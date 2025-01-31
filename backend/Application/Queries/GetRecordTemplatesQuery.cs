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
        => await dbContext.Set<RecordTemplate>()
            .Include(d => d.Sections)
                .ThenInclude(d => d.Fields)
            .ToArrayAsync();
}

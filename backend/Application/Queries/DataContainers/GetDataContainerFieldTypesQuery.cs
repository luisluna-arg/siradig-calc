using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.DataContainers;

public class GetDataContainerFieldTypesQuery() : IRequest<IEnumerable<FieldTypeDto>>
{
}

public class GetDataContainersQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<GetDataContainerFieldTypesQuery, IEnumerable<FieldTypeDto>>
{
    public async Task<IEnumerable<FieldTypeDto>> Handle(GetDataContainerFieldTypesQuery request, CancellationToken cancellationToken)
        => (await dbContext.FieldTypeMappings.ToArrayAsync()).Select(ftm => new FieldTypeDto(ftm)).ToArray();
}

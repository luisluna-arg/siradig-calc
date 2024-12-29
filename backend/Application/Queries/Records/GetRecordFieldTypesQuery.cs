using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Records;

public class GetRecordFieldTypesQuery() : IRequest<IEnumerable<FieldTypeDto>>
{
}

public class GetRecordsQueryHandler(ISolutionDbContext dbContext)
    : IRequestHandler<GetRecordFieldTypesQuery, IEnumerable<FieldTypeDto>>
{
    public async Task<IEnumerable<FieldTypeDto>> Handle(GetRecordFieldTypesQuery request, CancellationToken cancellationToken)
        => (await dbContext.FieldTypeMappings.ToArrayAsync()).Select(ftm => new FieldTypeDto(ftm)).ToArray();
}

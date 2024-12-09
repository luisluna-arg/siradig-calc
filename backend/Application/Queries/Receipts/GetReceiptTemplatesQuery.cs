using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptTemplatesQuery()
    : GetDataContainersQuery<ReceiptTemplate>()
{
}

public class GetReceiptTemplatesQueryHandler(ISolutionDbContext dbContext)
    : GetDataContainersQueryHandler<GetReceiptTemplatesQuery, ReceiptTemplate, ReceiptField>(dbContext)
{
}

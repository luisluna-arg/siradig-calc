using SiradigCalc.Application.Queries.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptTemplatesQuery()
    : GetDataContainersQuery<ReceiptTemplate>()
{
}

public class GetReceiptTemplatesQueryHandler(ISolutionDbContext dbContext)
    : GetDataContainersQueryHandler<GetReceiptTemplatesQuery, ReceiptTemplate, ReceiptTemplateSection, ReceiptField>(dbContext)
{
}

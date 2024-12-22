using SiradigCalc.Application.Queries.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptTemplateQuery(Guid id)
    : GetDataContainerQuery<ReceiptTemplate>(id)
{
}

public class GetReceiptTemplateQueryHandler(ISolutionDbContext dbContext)
    : GetDataContainerQueryHandler<GetReceiptTemplateQuery, ReceiptTemplate, ReceiptTemplateSection, ReceiptField>(dbContext)
{
}

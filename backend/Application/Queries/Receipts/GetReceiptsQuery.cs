using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptsQuery()
    : GetRecordInstancesQuery<Receipt>()
{
}

public class GetReceiptsQueryHandler(ISolutionDbContext dbContext)
    : GetRecordInstancesQueryHandler<GetReceiptsQuery, Receipt, ReceiptField, ReceiptValue, ReceiptTemplate, ReceiptTemplateSection>(dbContext)
{
}

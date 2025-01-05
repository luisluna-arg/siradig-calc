using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptQuery(Guid id)
    : GetRecordInstanceQuery<Receipt>(id)
{
}

public class GetReceiptQueryHandler(ISolutionDbContext dbContext)
    : GetRecordInstanceQueryHandler<GetReceiptQuery, Receipt, Guid, ReceiptField, ReceiptValue, ReceiptTemplate, ReceiptTemplateSection>(dbContext)
{
}

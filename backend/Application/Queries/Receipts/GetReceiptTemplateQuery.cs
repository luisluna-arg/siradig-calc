using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptTemplateQuery(Guid id)
    : GetRecordQuery<ReceiptTemplate>(id)
{
}

public class GetReceiptTemplateQueryHandler(ISolutionDbContext dbContext)
    : GetRecordQueryHandler<GetReceiptTemplateQuery, ReceiptTemplate, ReceiptTemplateSection, ReceiptField>(dbContext)
{
}

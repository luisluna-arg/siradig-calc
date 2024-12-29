using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptTemplatesQuery()
    : GetRecordsQuery<ReceiptTemplate>()
{
}

public class GetReceiptTemplatesQueryHandler(ISolutionDbContext dbContext)
    : GetRecordsQueryHandler<GetReceiptTemplatesQuery, ReceiptTemplate, ReceiptTemplateSection, ReceiptField>(dbContext)
{
}

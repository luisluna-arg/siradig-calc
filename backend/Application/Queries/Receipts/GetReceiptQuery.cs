using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptQuery(Guid id)
    : GetRecordInstanceQuery<Form>(id)
{
}

public class GetReceiptQueryHandler(ISolutionDbContext dbContext)
    : GetRecordInstanceQueryHandler<GetReceiptQuery, Form, FormTemplate, FormTemplateSection, FormField, FormValue>(dbContext)
{
}

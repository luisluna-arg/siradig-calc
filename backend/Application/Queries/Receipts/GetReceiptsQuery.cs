using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptsQuery()
    : GetRecordInstancesQuery<Form>()
{
}

public class GetReceiptsQueryHandler(ISolutionDbContext dbContext)
    : GetRecordInstancesQueryHandler<GetReceiptsQuery, Form, FormField, FormValue, FormTemplate, FormTemplateSection>(dbContext)
{
}

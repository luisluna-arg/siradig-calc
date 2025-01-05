using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormsQuery()
    : GetRecordInstancesQuery<Form>()
{
}

public class GetFormsQueryHandler(ISolutionDbContext dbContext)
    : GetRecordInstancesQueryHandler<GetFormsQuery, Form, Guid, FormField, FormValue, FormTemplate, FormTemplateSection>(dbContext)
{
}

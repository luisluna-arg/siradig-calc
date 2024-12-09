using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormsQuery()
    : GetDataContainerInstancesQuery<Form>()
{
}

public class GetFormsQueryHandler(ISolutionDbContext dbContext)
    : GetDataContainerInstancesQueryHandler<GetFormsQuery, Form, FormField, FormValue, FormTemplate>(dbContext)
{
}

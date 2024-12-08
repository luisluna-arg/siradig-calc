using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptsQuery()
    : GetDataContainerInstancesQuery<Form>()
{
}

public class GetReceiptsQueryHandler(ISolutionDbContext context)
    : GetDataContainerInstancesQueryHandler<GetReceiptsQuery, Form, FormField, FormValue, FormTemplate>(context)
{
}

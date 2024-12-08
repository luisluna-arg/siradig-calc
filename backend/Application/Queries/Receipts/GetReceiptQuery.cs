using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetReceiptQuery(Guid id)
    : GetDataContainerInstanceQuery<Form>(id)
{
}

public class GetReceiptQueryHandler(ISolutionDbContext context)
    : GetDataContainerInstanceQueryHandler<GetReceiptQuery, Form, FormField, FormValue, FormTemplate>(context)
{
}

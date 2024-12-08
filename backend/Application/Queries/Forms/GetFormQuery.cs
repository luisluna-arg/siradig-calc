using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormQuery(Guid id)
    : GetDataContainerInstanceQuery<Form>(id)
{
}

public class GetFormQueryHandler(ISolutionDbContext context)
    : GetDataContainerInstanceQueryHandler<GetFormQuery, Form, FormField, FormValue, FormTemplate>(context)
{
}

using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormTemplatesQuery()
    : GetDataContainersQuery<FormTemplate>()
{
}

public class GetFormTemplatesQueryHandler(ISolutionDbContext context)
    : GetDataContainersQueryHandler<GetFormTemplatesQuery, FormTemplate, FormField>(context)
{
}

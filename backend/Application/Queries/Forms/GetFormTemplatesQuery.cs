using SiradigCalc.Application.Queries.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormTemplatesQuery()
    : GetDataContainersQuery<FormTemplate>()
{
}

public class GetFormTemplatesQueryHandler(ISolutionDbContext dbContext)
    : GetDataContainersQueryHandler<GetFormTemplatesQuery, FormTemplate, FormTemplateSection, FormField>(dbContext)
{
}

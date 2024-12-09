using SiradigCalc.Application.Queries.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormTemplateQuery(Guid id)
    : GetDataContainerQuery<FormTemplate>(id)
{
}

public class GetFormTemplateQueryHandler(ISolutionDbContext dbContext)
    : GetDataContainerQueryHandler<GetFormTemplateQuery, FormTemplate, FormField>(dbContext)
{
}

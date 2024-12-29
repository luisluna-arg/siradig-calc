using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormTemplateQuery(Guid id)
    : GetRecordQuery<FormTemplate>(id)
{
}

public class GetFormTemplateQueryHandler(ISolutionDbContext dbContext)
    : GetRecordQueryHandler<GetFormTemplateQuery, FormTemplate, FormTemplateSection, FormField>(dbContext)
{
}

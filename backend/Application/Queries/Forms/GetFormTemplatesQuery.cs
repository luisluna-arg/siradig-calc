using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormTemplatesQuery()
    : GetRecordsQuery<FormTemplate>()
{
}

public class GetFormTemplatesQueryHandler(ISolutionDbContext dbContext)
    : GetRecordsQueryHandler<GetFormTemplatesQuery, FormTemplate, FormTemplateSection, FormField>(dbContext)
{
}

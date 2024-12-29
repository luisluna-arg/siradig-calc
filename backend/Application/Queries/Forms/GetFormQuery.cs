using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormQuery(Guid id)
    : GetRecordInstanceQuery<Form>(id)
{
}

public class GetFormQueryHandler(ISolutionDbContext dbContext)
    : GetRecordInstanceQueryHandler<GetFormQuery, Form, FormField, FormValue, FormTemplate, FormTemplateSection>(dbContext)
{
}

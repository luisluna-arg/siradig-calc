using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Forms;

public class GetFormQuery(Guid id)
    : GetRecordInstanceQuery<Form, Guid>(id)
{
}

public class GetFormQueryHandler(ISolutionDbContext dbContext)
    : GetRecordInstanceQueryHandler<GetFormQuery, Form, Guid, FormField, FormValue, FormTemplate, FormTemplateSection>(dbContext)
{
}

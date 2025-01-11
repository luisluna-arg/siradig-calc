using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class UpdateFormCommand()
    : UpdateRecordCommand<Guid, Guid>()
{
}

public class UpdateFormHandler(ISolutionDbContext dbContext)
    : UpdateRecordCommandHandler<UpdateFormCommand, Guid, Form, Guid, FormTemplate, FormTemplateSection, FormField, FormValue>(dbContext)
                                
{
}

public class UpdateFormCommandValidator(ISolutionDbContext dbContext)
    : UpdateRecordCommandValidator<UpdateFormCommand, FormTemplate, FormTemplateSection, FormField>(dbContext)
{
}
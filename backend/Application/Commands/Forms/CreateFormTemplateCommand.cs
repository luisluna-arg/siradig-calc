using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormTemplateCommand() : CreateRecordTemplateCommand()
{
}

public class CreateFormTemplateCommandHandler(ISolutionDbContext dbContext)
    : CreateRecordTemplateCommandHandler<CreateFormTemplateCommand, FormTemplate, FormTemplateSection, FormField>(dbContext)
{
}

public class CreateFormTemplateCommandValidator : CreateRecordTemplateCommandValidator
{
}
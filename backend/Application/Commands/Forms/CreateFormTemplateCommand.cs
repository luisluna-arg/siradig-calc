using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormTemplateCommand() : CreateDataContainerTemplateCommand()
{
}

public class CreateFormTemplateCommandHandler(ISolutionDbContext dbContext)
    : CreateDataContainerTemplateCommandHandler<CreateFormTemplateCommand, FormTemplate, FormTemplateSection, FormField>(dbContext)
{
}

public class CreateFormTemplateCommandValidator : CreateDataContainerTemplateCommandValidator
{
}
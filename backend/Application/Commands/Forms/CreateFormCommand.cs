using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormCommand()
    : CreateDataContainerCommand<CreateValueDto>()
{
}

/* TODO Fix so that type parameters match Validator */
public class CreateFormHandler(ISolutionDbContext dbContext)
    : CreateDataContainerCommandHandler<CreateFormCommand, Form, FormTemplateSection, FormField, FormValue, FormTemplate>(dbContext)
{
}

public class CreateFormCommandValidator(ISolutionDbContext dbContext)
    : CreateDataContainerCommandValidator<CreateFormCommand, CreateValueDto, FormTemplate, FormTemplateSection, FormField>(dbContext)
{
}
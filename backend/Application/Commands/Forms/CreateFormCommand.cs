using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormCommand()
    : CreateDataContainerCommand<CreateValueDto>()
{
}

public class CreateFormHandler(ISolutionDbContext dbContext)
    : CreateDataContainerCommandHandler<CreateFormCommand, Form, FormTemplateSection, FormField, FormValue, FormTemplate>(dbContext)
{
}

using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormValueCommand : CreateDataContainerValueCommand
{
}

/* TODO Fix so that it matches the validator class */
public class CreateFormValueHandler(ISolutionDbContext dbContext)
    : CreateDataContainerValueCommandHandler<CreateFormValueCommand, FormValue, FormField>(dbContext)
{
}

public class CreateFormValueCommandValidator(ISolutionDbContext dbContext)
    : CreateDataContainerValueCommandValidator<FormField, FormValue>(dbContext)
{
}
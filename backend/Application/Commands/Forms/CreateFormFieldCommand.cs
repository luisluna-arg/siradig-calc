using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormFieldCommand() : CreateDataContainerFieldCommand()
{
}

public class CreateFormFieldHandler(ISolutionDbContext dbContext)
    : CreateDataContainerFieldCommandHandler<CreateFormFieldCommand, FormField>(dbContext)
{
}

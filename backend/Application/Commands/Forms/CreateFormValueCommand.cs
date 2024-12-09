using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormValueCommand : CreateDataContainerValueCommand
{
}

public class CreateFormValueHandler(ISolutionDbContext dbContext)
    : CreateDataContainerValueCommandHandler<CreateFormValueCommand, FormValue, FormField>(dbContext)
{
}

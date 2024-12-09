using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class DeleteFormFieldCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteFormFieldCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteFormFieldCommand, FormField, Guid>(dbContext)
{
}

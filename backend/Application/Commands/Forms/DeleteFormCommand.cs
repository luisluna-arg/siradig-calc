using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class DeleteFormCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteFormCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteFormCommand, Form, Guid>(dbContext)
{
}

public class DeleteFormCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class DeleteFormValueCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteFormValueCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteFormValueCommand, FormValue, Guid>(dbContext)
{
}

public class DeleteFormValueCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
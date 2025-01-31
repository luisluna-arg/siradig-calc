using SiradigCalc.Application.Commands.Base;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class DeleteRecordCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteRecordCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteRecordCommand, Record, Guid>(dbContext)
{
}

public class DeleteRecordCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
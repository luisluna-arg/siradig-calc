using SiradigCalc.Application.Commands.Base;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class DeleteRecordValueCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteRecordValueCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteRecordValueCommand, RecordValue, Guid>(dbContext)
{
}

public class DeleteRecordValueCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
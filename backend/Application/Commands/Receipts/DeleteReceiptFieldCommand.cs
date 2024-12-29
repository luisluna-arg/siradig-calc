using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class DeleteReceiptFieldCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteReceiptFieldHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteReceiptFieldCommand, ReceiptField, Guid>(dbContext)
{
}

public class DeleteReceiptFieldCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
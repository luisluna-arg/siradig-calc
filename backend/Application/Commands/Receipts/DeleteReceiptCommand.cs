using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class DeleteReceiptCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteReceiptCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteReceiptCommand, Receipt, Guid>(dbContext)
{
}

public class DeleteReceiptCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
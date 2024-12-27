using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class DeleteReceiptValueCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteReceiptValueHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteReceiptValueCommand, ReceiptValue, Guid>(dbContext)
{
}

public class DeleteReceiptValueCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
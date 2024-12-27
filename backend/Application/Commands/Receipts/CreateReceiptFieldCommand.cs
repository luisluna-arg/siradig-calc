using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptFieldCommand() : CreateDataContainerFieldCommand()
{
}

public class CreateReceiptFieldHandler(ISolutionDbContext dbContext)
    : CreateDataContainerFieldCommandHandler<CreateReceiptFieldCommand, ReceiptField>(dbContext)
{
}

public class CreateReceiptFieldCommandValidator() : CreateDataContainerFieldCommandValidator()
{
}
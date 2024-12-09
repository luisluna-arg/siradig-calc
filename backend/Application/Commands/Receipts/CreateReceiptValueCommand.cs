using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptValueCommand : CreateDataContainerValueCommand
{
}

public class CreateReceiptValueHandler(ISolutionDbContext dbContext)
    : CreateDataContainerValueCommandHandler<CreateReceiptValueCommand, ReceiptValue, ReceiptField>(dbContext)
{
}

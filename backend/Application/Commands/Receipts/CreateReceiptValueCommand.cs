using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptValueCommand : CreateDataContainerValueCommand
{
}

/* TODO Fix type parameters so that they match the validator */
public class CreateReceiptValueHandler(ISolutionDbContext dbContext)
    : CreateDataContainerValueCommandHandler<CreateReceiptValueCommand, ReceiptValue, ReceiptField>(dbContext)
{
}

public class CreateReceiptValueCommandValidator(ISolutionDbContext dbContext) : CreateDataContainerValueCommandValidator<ReceiptField, ReceiptValue>(dbContext)
{
}
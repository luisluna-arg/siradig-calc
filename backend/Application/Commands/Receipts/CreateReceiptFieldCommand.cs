using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptFieldCommand() : CreateRecordFieldCommand()
{
}

public class CreateReceiptFieldHandler(ISolutionDbContext dbContext)
    : CreateRecordFieldCommandHandler<CreateReceiptFieldCommand, ReceiptField>(dbContext)
{
}

public class CreateReceiptFieldCommandValidator() : CreateRecordFieldCommandValidator()
{
}
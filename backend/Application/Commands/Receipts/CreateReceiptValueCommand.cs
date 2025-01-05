using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptValueCommand : CreateRecordValueCommand<Guid>
{
}

/* TODO Fix type parameters so that they match the validator */
public class CreateReceiptValueHandler(ISolutionDbContext dbContext)
    : CreateRecordValueCommandHandler<CreateReceiptValueCommand, Receipt, Guid, ReceiptTemplate, ReceiptTemplateSection, ReceiptField, ReceiptValue>(dbContext)
{
}

public class CreateReceiptValueCommandValidator(ISolutionDbContext dbContext)
    : CreateRecordValueCommandValidator<Receipt, Guid, ReceiptTemplate, ReceiptTemplateSection, ReceiptField, ReceiptValue>(dbContext)
{
}
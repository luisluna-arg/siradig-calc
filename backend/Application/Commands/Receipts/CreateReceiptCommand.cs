using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptCommand() : CreateRecordCommand<Guid, CreateValueDto>()
{
}

public class CreateReceiptHandler(ISolutionDbContext dbContext)
    : CreateRecordCommandHandler<CreateReceiptCommand, Receipt, Guid, ReceiptTemplate, ReceiptTemplateSection, ReceiptField, ReceiptValue>(dbContext)
{
}

public class CreateReceiptCommandValidator(ISolutionDbContext dbContext)
    : CreateRecordCommandValidator<CreateReceiptCommand, CreateValueDto, ReceiptTemplate, ReceiptTemplateSection, ReceiptField>(dbContext)
{
}
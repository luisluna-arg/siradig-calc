using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class UpdateReceiptCommand() : UpdateRecordCommand<Guid, Guid>()
{
}

public class UpdateReceiptHandler(ISolutionDbContext dbContext)
    : UpdateRecordCommandHandler<UpdateReceiptCommand, Guid, Receipt, Guid, ReceiptTemplate, ReceiptTemplateSection, ReceiptField, ReceiptValue>(dbContext)
{
}

public class UpdateReceiptCommandValidator(ISolutionDbContext dbContext)
    : UpdateRecordCommandValidator<UpdateReceiptCommand, ReceiptTemplate, ReceiptTemplateSection, ReceiptField>(dbContext)
{
}
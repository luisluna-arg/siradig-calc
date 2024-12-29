using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptTemplateCommand() : CreateRecordTemplateCommand()
{
}

public class CreateReceiptTemplateHandler(ISolutionDbContext dbContext)
    : CreateRecordTemplateCommandHandler<CreateReceiptTemplateCommand, ReceiptTemplate, ReceiptTemplateSection, ReceiptField>(dbContext)
{
}

public class CreateReceiptTemplateCommandValidator() : CreateRecordTemplateCommandValidator()
{
}
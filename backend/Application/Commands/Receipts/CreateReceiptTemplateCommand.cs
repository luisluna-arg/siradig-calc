using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptTemplateCommand() : CreateDataContainerTemplateCommand()
{
}

public class CreateReceiptTemplateHandler(ISolutionDbContext dbContext)
    : CreateDataContainerTemplateCommandHandler<CreateReceiptTemplateCommand, ReceiptTemplate, ReceiptTemplateSection, ReceiptField>(dbContext)
{
}

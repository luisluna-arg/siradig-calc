using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptCommand() : CreateDataContainerCommand<CreateValueDto>()
{
}

public class CreateReceiptHandler(ISolutionDbContext dbContext)
    : CreateDataContainerCommandHandler<CreateReceiptCommand, Receipt, ReceiptTemplateSection, ReceiptField, ReceiptValue, ReceiptTemplate>(dbContext)
{
}

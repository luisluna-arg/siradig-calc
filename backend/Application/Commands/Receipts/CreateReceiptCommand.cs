using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptCommand() : CreateDataContainerCommand<CreateValueDto>()
{
}

/* TODO Fix base class to match CreateDataContainerCommandValidator type parameters */
public class CreateReceiptHandler(ISolutionDbContext dbContext)
    : CreateDataContainerCommandHandler<CreateReceiptCommand, Receipt, ReceiptTemplateSection, ReceiptField, ReceiptValue, ReceiptTemplate>(dbContext)
{
}

public class CreateReceiptCommandValidator(ISolutionDbContext dbContext)
    : CreateDataContainerCommandValidator<CreateReceiptCommand, CreateValueDto, ReceiptTemplate, ReceiptTemplateSection, ReceiptField>(dbContext)
{
}
using MediatR;
using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptCommand()
    : CreateDataContainerCommand<CreateValueDto>(), IRequest<Guid>
{
}

public class CreateReceiptHandler(ISolutionDbContext context)
    : CreateDataContainerCommandHandler<CreateReceiptCommand, Receipt, ReceiptField, ReceiptValue, ReceiptTemplate>(context),
    IRequestHandler<CreateReceiptCommand, Guid>
{
}

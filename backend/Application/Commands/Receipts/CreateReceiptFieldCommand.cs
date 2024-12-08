using MediatR;
using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptFieldCommand() : CreateDataContainerFieldCommand(), IRequest<Guid>
{
}

public class CreateReceiptFieldHandler(ISolutionDbContext context)
    : CreateDataContainerFieldCommandHandler<CreateReceiptFieldCommand, ReceiptField>(context),
    IRequestHandler<CreateReceiptFieldCommand, Guid>
{
}

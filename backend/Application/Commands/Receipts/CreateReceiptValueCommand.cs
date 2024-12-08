using MediatR;
using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptValueCommand : CreateDataContainerValueCommand, IRequest<Guid>
{
}

public class CreateReceiptValueHandler(ISolutionDbContext context)
    : CreateDataContainerValueCommandHandler<CreateReceiptValueCommand, ReceiptValue, ReceiptField>(context),
    IRequestHandler<CreateReceiptValueCommand, Guid>
{
}

using MediatR;
using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class CreateReceiptTemplateCommand() : CreateDataContainerTemplateCommand(), IRequest<Guid>
{
}

public class CreateReceiptTemplateHandler(ISolutionDbContext context)
    : CreateDataContainerTemplateCommandHandler<CreateReceiptTemplateCommand, ReceiptTemplate, ReceiptField>(context),
    IRequestHandler<CreateReceiptTemplateCommand, Guid>
{
}

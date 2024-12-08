using MediatR;
using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormFieldCommand() : CreateDataContainerFieldCommand(), IRequest<Guid>
{
}

public class CreateFormFieldHandler(ISolutionDbContext context)
    : CreateDataContainerFieldCommandHandler<CreateFormFieldCommand, FormField>(context),
    IRequestHandler<CreateFormFieldCommand, Guid>
{
}

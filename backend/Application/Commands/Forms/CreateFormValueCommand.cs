using MediatR;
using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormValueCommand : CreateDataContainerValueCommand, IRequest<Guid>
{
}

public class CreateFormValueHandler(ISolutionDbContext context)
    : CreateDataContainerValueCommandHandler<CreateFormValueCommand, FormValue, FormField>(context),
    IRequestHandler<CreateFormValueCommand, Guid>
{
}

using MediatR;
using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormCommand()
    : CreateDataContainerCommand<CreateValueDto>(), IRequest<Guid>
{
}

public class CreateFormHandler(ISolutionDbContext context)
    : CreateDataContainerCommandHandler<CreateFormCommand, Form, FormField, FormValue, FormTemplate>(context),
    IRequestHandler<CreateFormCommand, Guid>
{
}

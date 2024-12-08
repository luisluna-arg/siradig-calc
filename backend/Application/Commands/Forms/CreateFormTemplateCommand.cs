using MediatR;
using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormTemplateCommand() : CreateDataContainerTemplateCommand(), IRequest<Guid>
{
}

public class CreateFormTemplateCommandHandler(ISolutionDbContext context)
    : CreateDataContainerTemplateCommandHandler<CreateFormTemplateCommand, FormTemplate, FormField>(context),
    IRequestHandler<CreateFormTemplateCommand, Guid>
{
}

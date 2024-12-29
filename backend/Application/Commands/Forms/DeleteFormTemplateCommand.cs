using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class DeleteFormTemplateCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteFormTemplateCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteFormTemplateCommand, FormTemplate, Guid>(dbContext)
{
}

public class DeleteFormTemplateCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
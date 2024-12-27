using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Core.Entities.Receipts;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class DeleteReceiptTemplateCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteReceiptTemplateHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteReceiptTemplateCommand, ReceiptTemplate, Guid>(dbContext)
{
}

public class DeleteReceiptTemplateCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
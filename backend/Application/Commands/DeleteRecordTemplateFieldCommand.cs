using SiradigCalc.Application.Commands.Base;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class DeleteRecordTemplateFieldCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteRecordTemplateFieldCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteRecordTemplateFieldCommand, RecordTemplateField, Guid>(dbContext)
{
}

public class DeleteRecordTemplateFieldCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
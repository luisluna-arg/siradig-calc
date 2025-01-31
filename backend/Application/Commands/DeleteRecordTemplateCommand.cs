using SiradigCalc.Application.Commands.Base;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class DeleteRecordTemplateCommand(Guid id) : DeleteInstanceCommand<Guid>(id)
{
}

public class DeleteRecordTemplateCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<DeleteRecordTemplateCommand, RecordTemplate, Guid>(dbContext)
{
}

public class DeleteRecordTemplateCommandValidator : DeleteInstanceCommandValidator<Guid>
{
}
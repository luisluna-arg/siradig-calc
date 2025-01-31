using System.Linq.Expressions;
using SiradigCalc.Application.Commands.Base;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UnlinkFieldTemplatesCommand(
    Guid leftTemplateId,
    Guid rightTemplateId,
    Guid leftFieldId,
    Guid rightFieldId
) : DeleteInstanceCommand<Guid>()
{
    public Guid LeftTemplateId { get; } = leftTemplateId;
    public Guid RightTemplateId { get; } = rightTemplateId;
    public Guid LeftFieldId { get; } = leftFieldId;
    public Guid RightFieldId { get; } = rightFieldId;
}

public class UnlinkFieldTemplatesCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<UnlinkFieldTemplatesCommand, RecordTemplateFieldLink, Guid>(dbContext)
{
    protected override Expression<Func<RecordTemplateFieldLink, bool>> CreateFilterFunc(UnlinkFieldTemplatesCommand command)
        => (e) => e.RightFieldId == command.RightFieldId && e.LeftFieldId == command.LeftFieldId;
}

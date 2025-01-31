using System.Linq.Expressions;
using SiradigCalc.Application.Commands.Base;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UnlinkTemplatesCommand(Guid leftTemplateId, Guid rightTemplateId) : DeleteInstanceCommand<Guid>()
{
    public Guid LeftTemplateId { get; } = leftTemplateId;
    public Guid RightTemplateId { get; } = rightTemplateId;
}

public class UnlinkTemplatesCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<UnlinkTemplatesCommand, RecordTemplateLink, Guid>(dbContext)
{
    protected override Expression<Func<RecordTemplateLink, bool>> CreateFilterFunc(UnlinkTemplatesCommand command)
        => (e) => e.RightTemplateId == command.RightTemplateId && e.LeftTemplateId == command.LeftTemplateId;
}

using System.Linq.Expressions;
using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class UnlinkTemplatesCommand(Guid receiptTemplateId, Guid formTemplateId) : DeleteInstanceCommand<Guid>()
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
    public Guid FormTemplateId { get; } = formTemplateId;
}

public class UnlinkTemplatesCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<UnlinkTemplatesCommand, RecordTemplateLink, Guid>(dbContext)
{
    protected override Expression<Func<RecordTemplateLink, bool>> CreateFilterFunc(UnlinkTemplatesCommand command)
        => (e) => e.FormTemplateId == command.FormTemplateId && e.ReceiptTemplateId == command.ReceiptTemplateId;
}

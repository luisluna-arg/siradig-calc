using System.Linq.Expressions;
using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class UnlinkFieldTemplatesCommand(
    Guid receiptTemplateId,
    Guid formTemplateId,
    Guid receiptFieldId,
    Guid formFieldId
) : DeleteInstanceCommand<Guid>()
{
    public Guid ReceiptTemplateId { get; } = receiptTemplateId;
    public Guid FormTemplateId { get; } = formTemplateId;
    public Guid ReceiptFieldId { get; } = receiptFieldId;
    public Guid FormFieldId { get; } = formFieldId;
}

public class UnlinkFieldTemplatesCommandHandler(ISolutionDbContext dbContext)
    : DeleteInstanceCommandHandler<UnlinkFieldTemplatesCommand, RecordFieldLink, Guid>(dbContext)
{
    protected override Expression<Func<RecordFieldLink, bool>> CreateFilterFunc(UnlinkFieldTemplatesCommand command)
        => (e) => e.FormFieldId == command.FormFieldId && e.ReceiptFieldId == command.ReceiptFieldId;
}

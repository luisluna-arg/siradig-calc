using MediatR;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Records;

public abstract class CreateRecordValueCommand<TRecordId>() : IRequest<Guid>
{
    public TRecordId RecordId { get; set; } = default!;
    public Guid FieldId { get; set; }
    public string Value { get; set; } = string.Empty;
}

public abstract class CreateRecordValueCommandHandler<TCommand, TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateRecordValueCommand<TRecordId>
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>
    where TRecordSection : BaseRecordSection<TField>
    where TRecord : BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>
    where TField : BaseRecordField, new()
    where TValue : BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
{
    public async Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var value = CreateInstance(command);

        await dbContext.Set<TValue>().AddAsync(value, cancellationToken);
        await dbContext.SaveChangesAsync();

        return value.Id;
    }

    protected virtual TValue CreateInstance(TCommand command)
        => new TValue
        {
            Id = Guid.NewGuid(),
            RecordId = command.RecordId,
            FieldId = command.FieldId,
            Value = command.Value
        };
}

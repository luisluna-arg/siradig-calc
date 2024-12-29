using MediatR;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Records;

public abstract class CreateRecordValueCommand() : IRequest<Guid>
{
    public Guid FieldId { get; set; }
    public string Value { get; set; } = string.Empty;
}

public abstract class CreateRecordValueCommandHandler<TCommand, TField, TValue>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateRecordValueCommand
    where TField : BaseRecordField, new()
    where TValue : BaseRecordValue<TField>, new()
{
    public async Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var value = CreateInstance(command);

        await dbContext.Set<TValue>().AddAsync(value, cancellationToken);
        await dbContext.SaveChangesAsync();

        return value.Id;
    }

    protected virtual TValue CreateInstance(CreateRecordValueCommand command)
        => new TValue
        {
            Id = Guid.NewGuid(),
            FieldId = command.FieldId,
            Value = command.Value
        };
}

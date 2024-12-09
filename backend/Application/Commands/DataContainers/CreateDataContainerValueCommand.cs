using MediatR;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.DataContainers;

public abstract class CreateDataContainerValueCommand() : IRequest<Guid>
{
    public Guid FieldId { get; set; }
    public string Value { get; set; } = string.Empty;
}

public abstract class CreateDataContainerValueCommandHandler<TCommand, TValue, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateDataContainerValueCommand
    where TField : BaseDataContainerField, new()
    where TValue : BaseDataContainerValue<TField>, new()
{
    public async Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var value = CreateInstance(command);

        await dbContext.Set<TValue>().AddAsync(value, cancellationToken);
        await dbContext.SaveChangesAsync();

        return value.Id;
    }

    protected virtual TValue CreateInstance(CreateDataContainerValueCommand command)
        => new TValue
        {
            Id = Guid.NewGuid(),
            FieldId = command.FieldId,
            Value = command.Value
        };
}

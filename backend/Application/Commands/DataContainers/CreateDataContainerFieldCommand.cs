using MediatR;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.DataContainers;

public abstract class CreateDataContainerFieldCommand() : IRequest<Guid>
{
    public string Label { get; set; } = string.Empty;
    public int FieldType { get; set; } = 0;
    public bool IsRequired { get; set; } = true;
}

public abstract class CreateDataContainerFieldCommandHandler<TCommand, TField>(ISolutionDbContext context)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateDataContainerFieldCommand
    where TField : BaseDataContainerField, new()
{
    public async Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var field = CreateInstance(command);

        await context.Set<TField>().AddAsync(field);
        await context.SaveChangesAsync();

        return field.Id;
    }

    protected virtual TField CreateInstance(CreateDataContainerFieldCommand command)
        => new TField
        {
            Id = Guid.NewGuid(),
            Label = command.Label,
            FieldType = (FieldType)command.FieldType,
            IsRequired = command.IsRequired
        };
}

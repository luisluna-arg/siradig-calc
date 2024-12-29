using MediatR;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Records;

public abstract class CreateRecordFieldCommand() : IRequest<Guid>
{
    public string Label { get; set; } = string.Empty;
    public int FieldType { get; set; } = 0;
    public bool IsRequired { get; set; } = true;
}

public abstract class CreateRecordFieldCommandHandler<TCommand, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateRecordFieldCommand
    where TField : BaseRecordField, new()
{
    public async Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var field = CreateInstance(command);

        await dbContext.Set<TField>().AddAsync(field);
        await dbContext.SaveChangesAsync();

        return field.Id;
    }

    protected virtual TField CreateInstance(CreateRecordFieldCommand command)
        => new TField
        {
            Id = Guid.NewGuid(),
            Label = command.Label,
            FieldType = (FieldType)command.FieldType,
            IsRequired = command.IsRequired
        };
}

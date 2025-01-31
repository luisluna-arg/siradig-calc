using MediatR;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public abstract class CreateRecordValueCommand() : IRequest<Guid>
{
    public Guid RecordId { get; set; } = default!;
    public Guid FieldId { get; set; }
    public string Value { get; set; } = string.Empty;
}

public abstract class CreateRecordValueCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<CreateRecordValueCommand, Guid>
{
    public async Task<Guid> Handle(CreateRecordValueCommand command, CancellationToken cancellationToken)
    {
        var value = new RecordValue
        {
            Id = Guid.NewGuid(),
            RecordId = command.RecordId,
            FieldId = command.FieldId,
            Value = command.Value
        };

        await dbContext.RecordValues.AddAsync(value, cancellationToken);
        await dbContext.SaveChangesAsync();

        return value.Id;
    }
}

using MediatR;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public abstract class CreateRecordTemplateFieldCommand() : IRequest<Guid>
{
    public string Label { get; set; } = string.Empty;
    public int FieldType { get; set; } = 0;
    public bool IsRequired { get; set; } = true;
}

public abstract class CreateRecordFieldCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<CreateRecordTemplateFieldCommand, Guid>
{
    public async Task<Guid> Handle(CreateRecordTemplateFieldCommand command, CancellationToken cancellationToken)
    {
        var field = new RecordTemplateField
        {
            Id = Guid.NewGuid(),
            Label = command.Label,
            FieldType = (FieldType)command.FieldType,
            IsRequired = command.IsRequired
        };

        await dbContext.RecordTemplateFields.AddAsync(field);
        await dbContext.SaveChangesAsync();

        return field.Id;
    }
}

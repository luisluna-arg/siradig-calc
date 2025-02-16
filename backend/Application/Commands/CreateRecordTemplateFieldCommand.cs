using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class CreateRecordTemplateFieldCommand() : IRequest<Guid>
{
    [JsonIgnore]
    public Guid RecordTemplateId { get; set; }
    [JsonIgnore]
    public Guid RecordTemplateSectionId { get; set; }
    public string Label { get; set; } = string.Empty;
    public int FieldType { get; set; } = 0;
    public bool IsRequired { get; set; } = true;
}

public class CreateRecordFieldCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<CreateRecordTemplateFieldCommand, Guid>
{
    public async Task<Guid> Handle(CreateRecordTemplateFieldCommand command, CancellationToken cancellationToken)
    {
        var section = await (from t in dbContext.RecordTemplates
            where t.Id == command.RecordTemplateId
            from s in t.Sections
            where s.Id == command.RecordTemplateSectionId
            select s).SingleAsync(cancellationToken);

        var newField = new RecordTemplateField
        {
            Id = Guid.NewGuid(),
            Label = command.Label,
            FieldType = (FieldType)command.FieldType,
            IsRequired = command.IsRequired
        };

        section.Fields.Add(newField);

        await dbContext.RecordTemplateFields.AddAsync(newField);
        await dbContext.SaveChangesAsync();

        return newField.Id;
    }
}

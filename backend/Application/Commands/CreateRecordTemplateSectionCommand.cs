using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class CreateRecordTemplateSectionCommand() : IRequest<Guid>
{
    [JsonIgnore]
    public Guid RecordTemplateId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<CreateFieldDto> Fields { get; set; } = [];
}

public class CreateRecordTemplateSectionCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<CreateRecordTemplateSectionCommand, Guid>
{
    public async Task<Guid> Handle(CreateRecordTemplateSectionCommand command, CancellationToken cancellationToken)
    {
        var recordTemplate = await dbContext.RecordTemplates.FirstAsync(cancellationToken);

        var newField = new RecordTemplateSection
        {
            Name = command.Name,
            Fields = command.Fields.Select(f => new RecordTemplateField()
            {
                Label = f.Label,
                FieldType = (FieldType)f.FieldType,
                IsRequired = f.IsRequired
            }).ToArray(),
        };

        await dbContext.RecordTemplateSections.AddAsync(newField);
        await dbContext.SaveChangesAsync();

        return newField.Id;
    }
}

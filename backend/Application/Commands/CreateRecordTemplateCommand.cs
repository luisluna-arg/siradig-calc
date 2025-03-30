using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class CreateRecordTemplateCommand() : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<CreateSectionDto> Sections { get; set; } = [];
}

public class CreateRecordTemplateCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<CreateRecordTemplateCommand, Guid>
{
    public async virtual Task<Guid> Handle(CreateRecordTemplateCommand command, CancellationToken cancellationToken)
    {
        var fieldContainerTemplate = new RecordTemplate
        {
            Name = command.Name,
            Description = command.Description,
            Sections = command.Sections.Select(f => new RecordTemplateSection
            {
                Name = f.Name,
                Fields = f.Fields.Select(f => new RecordTemplateField()
                {
                    Label = f.Label,
                    FieldType = (FieldType)f.FieldType,
                    IsRequired = f.IsRequired
                }).ToArray()
            }).ToList()
        }; ;

        dbContext.RecordTemplates.Add(fieldContainerTemplate);
        await dbContext.SaveChangesAsync();

        return fieldContainerTemplate.Id;
    }
}

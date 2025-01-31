using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public abstract class CreateRecordTemplateCommand() : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<CreateSectionDto> Sections { get; set; } = new();
}

public abstract class CreateRecordTemplateCommandHandler<TCommand, TRecord, TRecordSection, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<CreateRecordTemplateCommand, Guid>
{
    public async virtual Task<Guid> Handle(CreateRecordTemplateCommand command, CancellationToken cancellationToken)
    {
        var fieldContainerTemplate = new RecordTemplate
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Sections = command.Sections.Select(f => new RecordTemplateSection
            {
                Id = Guid.NewGuid(),
                Name = f.Name,
                Fields = f.Fields.Select(f => new RecordTemplateField()
                {
                    Id = Guid.NewGuid(),
                    Label = f.Label,
                    FieldType = (FieldType)f.FieldType,
                    IsRequired = f.IsRequired
                }).ToArray()
            }).ToList()
        };;

        dbContext.RecordTemplates.Add(fieldContainerTemplate);
        await dbContext.SaveChangesAsync();

        return fieldContainerTemplate.Id;
    }
}

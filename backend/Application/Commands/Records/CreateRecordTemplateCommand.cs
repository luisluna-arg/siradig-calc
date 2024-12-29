using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Records;

public abstract class CreateRecordTemplateCommand() : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<CreateSectionDto> Sections { get; set; } = new();
}

public abstract class CreateRecordTemplateCommandHandler<TCommand, TRecord, TRecordSection, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateRecordTemplateCommand
    where TField : BaseRecordField, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TRecord : BaseRecordTemplate<TRecordSection, TField>, new()
{
    public async virtual Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var fieldContainerTemplate = CreateInstanceBinder(command);

        dbContext.Set<TRecord>().Add(fieldContainerTemplate);
        await dbContext.SaveChangesAsync();

        return fieldContainerTemplate.Id;
    }

    protected virtual TRecord CreateInstanceBinder(CreateRecordTemplateCommand command)
        => new TRecord
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Sections = command.Sections.Select(f => new TRecordSection
            {
                Id = Guid.NewGuid(),
                Name = f.Name,
                Fields = f.Fields.Select(f => new TField()
                {
                    Id = Guid.NewGuid(),
                    Label = f.Label,
                    FieldType = (FieldType)f.FieldType,
                    IsRequired = f.IsRequired
                }).ToArray()
            }).ToList()
        };
}

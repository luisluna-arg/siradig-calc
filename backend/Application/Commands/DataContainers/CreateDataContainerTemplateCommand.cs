using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Core.Entities.Enums;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.DataContainers;

public abstract class CreateDataContainerTemplateCommand() : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<CreateSectionDto> Sections { get; set; } = new();
}

public abstract class CreateDataContainerTemplateCommandHandler<TCommand, TDataContainer, TDataContainerSection, TField>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateDataContainerTemplateCommand
    where TField : BaseDataContainerField, new()
    where TDataContainerSection : BaseDataContainerSection<TField>, new()
    where TDataContainer : BaseDataContainer<TDataContainerSection, TField>, new()
{
    public async virtual Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var fieldContainerTemplate = CreateInstanceBinder(command);

        dbContext.Set<TDataContainer>().Add(fieldContainerTemplate);
        await dbContext.SaveChangesAsync();

        return fieldContainerTemplate.Id;
    }

    protected virtual TDataContainer CreateInstanceBinder(CreateDataContainerTemplateCommand command)
        => new TDataContainer
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Sections = command.Sections.Select(f => new TDataContainerSection
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

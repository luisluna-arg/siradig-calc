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
    public List<CreateFieldDto> Fields { get; set; } = new();
}

public abstract class CreateDataContainerTemplateCommandHandler<TCommand, TDataContainer, TField>(ISolutionDbContext context)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateDataContainerTemplateCommand
    where TField : BaseDataContainerField, new()
    where TDataContainer : BaseDataContainer<TField>, new()
{
    public async virtual Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var fieldContainerTemplate = CreateInstanceBinder(command);

        context.Set<TDataContainer>().Add(fieldContainerTemplate);
        await context.SaveChangesAsync();

        return fieldContainerTemplate.Id;
    }

    protected virtual TDataContainer CreateInstanceBinder(CreateDataContainerTemplateCommand command)
        => new TDataContainer
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            Fields = command.Fields.Select(f => new TField
            {
                Id = Guid.NewGuid(),
                Label = f.Label,
                FieldType = (FieldType)f.FieldType,
                IsRequired = f.IsRequired
            }).ToList()
        };
}

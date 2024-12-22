using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.DataContainers;

public abstract class CreateDataContainerCommand<TValueDto> : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid TemplateId { get; set; } = Guid.Empty;
    public List<TValueDto> Values { get; set; } = new();
}

public abstract class CreateDataContainerCommandHandler<TCommand, TDataContainer, TDataContainerSection, TField, TFieldValue, TDataContainerTemplate>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateDataContainerCommand<CreateValueDto>
    where TField : BaseDataContainerField, new()
    where TFieldValue : BaseDataContainerValue<TField>, new()
    where TDataContainerSection : BaseDataContainerSection<TField>, new()
    where TDataContainerTemplate : BaseDataContainer<TDataContainerSection, TField>, new()
    where TDataContainer : BaseDataContainerInstance<TDataContainerTemplate, Guid, TDataContainerSection, TFieldValue, TField>, new()
{
    public async virtual Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var entity = new TDataContainer();
        MapCommandToEntity(command, entity);

        var entityValues = command.Values.Select(MapValue).ToList();
        AssignValuesToEntity(entity, entityValues);

        await AddEntityToDbSet(dbContext, entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return GetEntityId(entity);
    }

    protected virtual void MapCommandToEntity(TCommand command, TDataContainer entity)
    {
        entity.Id = Guid.NewGuid();
        entity.DataContainerId = command.TemplateId;
    }

    protected virtual TFieldValue MapValue(CreateValueDto dto)
        => new TFieldValue
        {
            Id = Guid.NewGuid(),
            FieldId = dto.FieldId,
            Value = dto.Value,
        };

    protected virtual void AssignValuesToEntity(TDataContainer entity, List<TFieldValue> values)
    {
        entity.Values = values;
    }

    protected virtual async Task AddEntityToDbSet(ISolutionDbContext dbContext, TDataContainer entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<TDataContainer>().AddAsync(entity, cancellationToken);
    }

    protected virtual Guid GetEntityId(TDataContainer entity) => entity.Id;
}

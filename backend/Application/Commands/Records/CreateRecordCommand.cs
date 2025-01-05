using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Records;

public abstract class CreateRecordCommand<TGuid, TValueDto> : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TGuid TemplateId { get; set; } = default!;
    public List<TValueDto> Values { get; set; } = new();
}

public abstract class CreateRecordCommandHandler<TCommand, TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand, Guid>
    where TCommand : CreateRecordCommand<TRecordId, CreateValueDto>
    where TField : BaseRecordField, new()
    where TValue : BaseRecordValue<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
    where TRecord : BaseRecordInstance<TRecord, TRecordId, TRecordTemplate, TRecordSection, TField, TValue>, new()
{
    public async virtual Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var entity = new TRecord();
        MapCommandToEntity(command, entity);

        var entityValues = command.Values.Select(MapValue).ToList();
        AssignValuesToEntity(entity, entityValues);

        await AddEntityToDbSet(dbContext, entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return GetEntityId(entity);
    }

    protected virtual void MapCommandToEntity(TCommand command, TRecord entity)
    {
        entity.Id = Guid.NewGuid();
        entity.RecordId = command.TemplateId;
    }

    protected virtual TValue MapValue(CreateValueDto dto)
        => new TValue
        {
            Id = Guid.NewGuid(),
            FieldId = dto.FieldId,
            Value = dto.Value,
        };

    protected virtual void AssignValuesToEntity(TRecord entity, List<TValue> values)
    {
        entity.Values = values;
    }

    protected virtual async Task AddEntityToDbSet(ISolutionDbContext dbContext, TRecord entity, CancellationToken cancellationToken)
    {
        await dbContext.Set<TRecord>().AddAsync(entity, cancellationToken);
    }

    protected virtual Guid GetEntityId(TRecord entity) => entity.Id;
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Records;

public abstract class UpdateRecordCommand<TRecordId, TRecordTemplateId> : IRequest
{
    public TRecordId Id { get; set; } = default!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TRecordTemplateId TemplateId { get; set; } = default!;
}

public abstract class UpdateRecordCommandHandler<TCommand, TRecordId, TRecord, TRecordTemplateId, TRecordTemplate, TRecordSection, TField, TValue>(ISolutionDbContext dbContext)
    : IRequestHandler<TCommand>
    where TCommand : UpdateRecordCommand<TRecordId, TRecordTemplateId>
    where TField : BaseRecordField, new()
    where TValue : BaseRecordValue<TRecord, TRecordTemplateId, TRecordTemplate, TRecordSection, TField, TValue>, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
    where TRecord : BaseRecordInstance<TRecord, TRecordTemplateId, TRecordTemplate, TRecordSection, TField, TValue>, new()
{
    public async virtual Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<TRecord>()
            .FirstAsync(v => v.Id.Equals(command.Id), cancellationToken);

        MapCommandToEntity(command, entity);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    protected virtual void MapCommandToEntity(TCommand command, TRecord entity)
    {
        entity.Id = Guid.NewGuid();
        entity.RecordTemplateId = command.TemplateId;
        entity.Title = command.Title;
    }

    Task<Unit> IRequestHandler<TCommand, Unit>.Handle(TCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

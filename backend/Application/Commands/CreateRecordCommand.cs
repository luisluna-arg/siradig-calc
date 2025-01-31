using MediatR;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public abstract class CreateRecordCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public Guid TemplateId { get; set; } = default!;
    public List<CreateValueDto> Values { get; set; } = new();
}

public abstract class CreateRecordCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<CreateRecordCommand, Guid>
{
    public async virtual Task<Guid> Handle(CreateRecordCommand command, CancellationToken cancellationToken)
    {
        var entity = new Record();
        entity.Id = Guid.NewGuid();
        entity.TemplateId = command.TemplateId;
        entity.Title = command.Title;
        entity.Values = command.Values.Select(v => new RecordValue
        {
            Id = Guid.NewGuid(),
            FieldId = v.FieldId,
            Value = v.Value,
        }).ToList();

        await dbContext.Records.AddAsync(entity, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
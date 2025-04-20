using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UpdateRecordCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; } = default!;
    public string Title { get; set; } = string.Empty;
    public Guid TemplateId { get; set; } = default!;
    public List<UpdateValueDto>? Values { get; set; } = new();
}

public class UpdateRecordCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<UpdateRecordCommand>
{
    public async virtual Task<Unit> Handle(UpdateRecordCommand command, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Records
            .Include(r => r.Values)
            .FirstAsync(v => v.Id.Equals(command.Id), cancellationToken);

        entity.TemplateId = command.TemplateId;
        entity.Title = command.Title;

        var commandValues = command.Values ?? [];

        var valuesToDelete = entity.Values
            .Where(existing => !commandValues.Any(updated => updated.Id == existing.Id))
            .ToList();

        foreach (var value in valuesToDelete)
        {
            entity.Values.Remove(value);
        }

        foreach (var valueToAdd in commandValues.Where(newValue => newValue.Id == Guid.Empty))
        {
            entity.Values.Add(new() { FieldId = valueToAdd.FieldId, Value = valueToAdd.Value });
        }

        foreach (var existingValue in entity.Values)
        {
            var updatedValue = commandValues.FirstOrDefault(updated => updated.Id == existingValue.Id);
            if (updatedValue != null)
            {
                existingValue.Value = updatedValue.Value;
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

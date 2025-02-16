using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UpdateRecordCommand : IRequest
{
    [JsonIgnore]
    public Guid Id { get; set; } = default!;
    public string Title { get; set; } = string.Empty;
    public Guid TemplateId { get; set; } = default!;
}

public class UpdateRecordCommandHandler(ISolutionDbContext dbContext)
    : IRequestHandler<UpdateRecordCommand>
{
    public async virtual Task<Unit> Handle(UpdateRecordCommand command, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Records
            .FirstAsync(v => v.Id.Equals(command.Id), cancellationToken);

        entity.TemplateId = command.TemplateId;
        entity.Title = command.Title;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

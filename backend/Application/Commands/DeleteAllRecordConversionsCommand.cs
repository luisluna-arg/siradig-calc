using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class DeleteAllRecordConversionsCommand(Guid sourceId)
    : IRequest
{
    [JsonIgnore]
    public Guid SourceId { get; set; } = sourceId;
}

public class DeleteAllRecordConversionsCommandHandler(
    ISolutionDbContext dbContext)
    : IRequestHandler<DeleteAllRecordConversionsCommand>
{
    private ISolutionDbContext _dbContext = dbContext;

    public async Task<Unit> Handle(DeleteAllRecordConversionsCommand request, CancellationToken cancellationToken)
    {
        var recordConversion = await _dbContext.RecordConversions
            .Where(r => r.SourceId == request.SourceId)
            .ToListAsync(cancellationToken);

        _dbContext.RecordConversions.RemoveRange(recordConversion);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

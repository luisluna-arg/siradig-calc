using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class DeleteRecordConversionCommand(Guid sourceId, Guid conversionId)
    : IRequest
{
    [JsonIgnore]
    public Guid SourceId { get; set; } = sourceId;
    [JsonIgnore]
    public Guid ConversionId { get; set; } = conversionId;
}

public class DeleteRecordConversionCommandHandler(
    ISolutionDbContext dbContext)
    : IRequestHandler<DeleteRecordConversionCommand>
{
    private ISolutionDbContext _dbContext = dbContext;

    public async Task<Unit> Handle(DeleteRecordConversionCommand request, CancellationToken cancellationToken)
    {
        var recordConversion = await _dbContext.RecordConversions
            .FirstAsync(r => r.SourceId == request.SourceId && r.Id == request.ConversionId);

        _dbContext.RecordConversions.Remove(recordConversion);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

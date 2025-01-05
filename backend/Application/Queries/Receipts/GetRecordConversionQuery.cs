using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Converters;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Queries.Receipts;

public class GetRecordConversionQuery(Guid sourceId, Guid targetId)
    : IRequest<IRecordConversionDto>
{
    [JsonIgnore]
    public Guid SourceId { get; set; } = sourceId;

    [JsonIgnore]
    public Guid TargetId { get; set; } = targetId;
}

public class GetRecordConversionQueryHandler(ISolutionDbContext dbContext, IRecordConverter recordConverter)
    : IRequestHandler<GetRecordConversionQuery, IRecordConversionDto>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IRecordConverter _recordConverter = recordConverter;

    public async Task<IRecordConversionDto> Handle(GetRecordConversionQuery request, CancellationToken cancellationToken)
    {
        var source = await _dbContext.Records
            .SingleAsync(r => r.Id == request.SourceId, cancellationToken);

        var target = await _dbContext.Records
            .SingleAsync(r => r.Id == request.TargetId, cancellationToken);

        return await _recordConverter.Convert(source, target, cancellationToken);
    }
}

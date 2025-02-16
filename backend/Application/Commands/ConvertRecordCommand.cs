using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Converters;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Dtos.Conversion.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class ConvertRecordCommand(Guid sourceId, Guid targetTemplateId)
    : IRequest<IRecordConversionBaseDto>
{
    [JsonIgnore]
    public Guid SourceId { get; set; } = sourceId;

    [JsonIgnore]
    public Guid TargetTemplateId { get; set; } = targetTemplateId;
}

public class ConvertRecordCommandHandler(
    ISolutionDbContext dbContext,
    IRecordConverter recordConverter,
    IDtoMappingService mapperManager)
    : IRequestHandler<ConvertRecordCommand, IRecordConversionBaseDto>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IRecordConverter _recordConverter = recordConverter;

    public async Task<IRecordConversionBaseDto> Handle(ConvertRecordCommand request, CancellationToken cancellationToken)
    {
        var source = await _dbContext.Records
            .SingleAsync(r => r.Id == request.SourceId, cancellationToken);

        var target = await _dbContext.RecordTemplates
            .SingleAsync(r => r.Id == request.TargetTemplateId, cancellationToken);

        var newRecord = await _recordConverter.Convert(source, target, cancellationToken);

        await _dbContext.Records.AddAsync(newRecord, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return mapperManager.Map<RecordTemplateConversionDto>(newRecord.ConvertedFrom.First());
    }
}

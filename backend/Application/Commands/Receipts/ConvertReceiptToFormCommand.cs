using System.Text.Json.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Converters;
using SiradigCalc.Application.Dtos.Conversion;
using SiradigCalc.Application.Dtos.Conversion.Base;
using SiradigCalc.Application.Mapping;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Receipts;

public class ConvertReceiptToFormCommand(Guid sourceId, Guid targetTemplateId)
    : IRequest<IRecordConversionDto>
{
    [JsonIgnore]
    public Guid SourceId { get; set; } = sourceId;

    [JsonIgnore]
    public Guid TargetTemplateId { get; set; } = targetTemplateId;
}

public class ConvertReceiptToFormCommandHandler(
    ISolutionDbContext dbContext,
    IRecordConverter recordConverter,
    IDtoMappingService mapperManager)
    : IRequestHandler<ConvertReceiptToFormCommand, IRecordConversionDto>
{
    private ISolutionDbContext _dbContext = dbContext;
    private IRecordConverter _recordConverter = recordConverter;

    public async Task<IRecordConversionDto> Handle(ConvertReceiptToFormCommand request, CancellationToken cancellationToken)
    {
        var source = await _dbContext.Receipts
            .SingleAsync(r => r.Id == request.SourceId, cancellationToken);

        var target = await _dbContext.FormTemplates
            .SingleAsync(r => r.Id == request.TargetTemplateId, cancellationToken);

        var newForm = (Form)await _recordConverter.Convert(source, target, cancellationToken);

        await _dbContext.Forms.AddAsync(newForm, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return mapperManager.Map<ReceiptToFormConversionDto>(newForm.ReceiptToFormConversions.First());
    }
}

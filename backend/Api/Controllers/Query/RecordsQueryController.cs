using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries;

namespace SiradigCalc.Api.Controllers.Query;

[Route("api/records")]
[ApiController]
public class RecordsQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetRecords([FromQuery] Guid? templateId)
        => Ok(await Mediator.Send(new GetRecordsQuery(templateId)));

    [HttpGet("{recordId}")]
    public async Task<IActionResult> GetRecord(Guid recordId)
        => Ok(await Mediator.Send(new GetRecordQuery(recordId)));

    [HttpGet("conversions")]
    public async Task<IActionResult> GetRecordConversion([FromQuery] Guid? sourceId)
        => base.Ok(await Mediator.Send(new GetRecordTemplateConversionsQuery(sourceId)));

    [HttpGet("conversions/{conversionId}")]
    public async Task<IActionResult> GetRecordConversion(Guid conversionId)
        => base.Ok(await Mediator.Send(new GetRecordConversionQuery(conversionId)));

    [HttpGet("conversions/{sourceId}/to/{targetId}")]
    public async Task<IActionResult> GetRecordConversion(Guid sourceId, Guid targetId)
        => base.Ok(await Mediator.Send(new GetRecordToRecordTemplateConversionsQuery(sourceId, targetId)));
}

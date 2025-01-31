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
    public async Task<IActionResult> GetRecords()
        => Ok(await Mediator.Send(new GetRecordInstancesQuery()));

    [HttpGet("{recordId}")]
    public async Task<IActionResult> GetRecord(Guid recordId)
        => Ok(await Mediator.Send(new GetRecordQuery(recordId)));

    [HttpGet("convertions/{sourceId}")]
    public async Task<IActionResult> GetRecordConversion(Guid sourceId)
        => base.Ok(await Mediator.Send(new GetRecordTemplateConversionsQuery(sourceId)));

    [HttpGet("convertions{sourceId}/to/{targetId}")]
    public async Task<IActionResult> GetRecordConversion(Guid sourceId, Guid targetId)
        => base.Ok(await Mediator.Send(new GetRecordToRecordTemplateConversionsQuery(sourceId, targetId)));
}

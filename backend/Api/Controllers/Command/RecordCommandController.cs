using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Commands;

namespace SiradigCalc.Api.Controllers.Command;

[Route("api/records")]
[ApiController]
public class RecordCommandController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> CreateRecord(CreateRecordCommand command)
        => Ok(await Mediator.Send(command));

    [HttpPut("{recordId}")]
    public async Task<IActionResult> UpdateRecord(UpdateRecordCommand command, Guid recordId)
    {
        command.Id = recordId;
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{recordId}")]
    public async Task<IActionResult> DeleteRecord(Guid recordId)
        => Ok(await Mediator.Send(new DeleteRecordCommand(recordId)));

    [HttpPost("value")]
    public async Task<IActionResult> CreateRecordValue(CreateRecordValueCommand command)
        => Ok(await Mediator.Send(command));

    [HttpDelete("value/{recordId}")]
    public async Task<IActionResult> DeleteRecordValue(Guid valueId)
        => Ok(await Mediator.Send(new DeleteRecordValueCommand(valueId)));

    [HttpPost("conversions/{sourceId}/to/{targetTemplateId}")]
    public async Task<IActionResult> GetRecordConversion(Guid sourceId, Guid targetTemplateId)
        => base.Ok(await Mediator.Send(new ConvertRecordCommand(sourceId, targetTemplateId)));
    
    [HttpDelete("{sourceId}/conversions")]
    public async Task<IActionResult> DeleteAllRecordConversions(Guid sourceId)
        => base.Ok(await Mediator.Send(new DeleteAllRecordConversionsCommand(sourceId)));

    [HttpDelete("{sourceId}/conversions/{conversionId}")]
    public async Task<IActionResult> DeleteAllRecordConversions(Guid sourceId, Guid conversionId)
        => base.Ok(await Mediator.Send(new DeleteRecordConversionCommand(sourceId, conversionId)));
}

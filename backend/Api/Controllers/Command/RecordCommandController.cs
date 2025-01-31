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
}

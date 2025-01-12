using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Commands.Receipts;

namespace SiradigCalc.Api.Controllers.Command;

[Route("api/receipts")]
[ApiController]
public class ReceiptCommandController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> CreateReceipt(CreateReceiptCommand command)
        => Ok(await Mediator.Send(command));

    [HttpPut("{receiptId}")]
    public async Task<IActionResult> UpdateReceipt(UpdateReceiptCommand command, Guid receiptId)
    {
        command.Id = receiptId;
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{receiptId}")]
    public async Task<IActionResult> DeleteReceipt(Guid receiptId)
        => Ok(await Mediator.Send(new DeleteReceiptCommand(receiptId)));

    [HttpPost("value")]
    public async Task<IActionResult> CreateReceiptValue(CreateReceiptValueCommand command)
        => Ok(await Mediator.Send(command));

    [HttpDelete("value/{receiptValueId}")]
    public async Task<IActionResult> DeleteReceiptValue(Guid receiptValueId)
        => Ok(await Mediator.Send(new DeleteReceiptValueCommand(receiptValueId)));
    
    [HttpPost("convert/{sourceId}/to/{targetTemplateId}")]
    public async Task<IActionResult> GetRecordConversion(Guid sourceId, Guid targetTemplateId)
        => Ok(await Mediator.Send(new ConvertReceiptToFormCommand(sourceId, targetTemplateId)));
}

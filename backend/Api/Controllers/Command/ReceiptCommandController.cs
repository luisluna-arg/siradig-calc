using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Commands.Receipts;

namespace SiradigCalc.Api.Controllers;

[Route("api/receipts")]
[ApiController]
public class ReceiptCommandController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> CreateReceipt(CreateReceiptCommand command)
        => Ok(await Mediator.Send(command));

    [HttpPost("value")]
    public async Task<IActionResult> CreateReceiptValue(CreateReceiptValueCommand command)
        => Ok(await Mediator.Send(command));
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries.Receipts;

namespace SiradigCalc.Api.Controllers;

[Route("api/receipts")]
[ApiController]
public class ReceiptQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("{receiptId}")]
    public async Task<IActionResult> GetReceipt(Guid receiptId)
        => Ok(await Mediator.Send(new GetReceiptQuery(receiptId)));
    
    [HttpGet]
    public async Task<IActionResult> CreateReceipts()
        => Ok(await Mediator.Send(new GetReceiptsQuery()));
}

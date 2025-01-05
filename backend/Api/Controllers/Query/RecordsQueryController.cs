using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries.Forms;
using SiradigCalc.Application.Queries.Receipts;

namespace SiradigCalc.Api.Controllers.Query;

[Route("api/records")]
[ApiController]
public class RecordsQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("forms")]
    public async Task<IActionResult> GetForms()
        => Ok(await Mediator.Send(new GetFormsQuery()));

    [HttpGet("forms/{formId}")]
    public async Task<IActionResult> GetForm(Guid formId)
        => Ok(await Mediator.Send(new GetFormQuery(formId)));

    [HttpGet("receipts")]
    public async Task<IActionResult> GetReceipts()
        => Ok(await Mediator.Send(new GetReceiptsQuery()));

    [HttpGet("receipts/{receiptId}")]
    public async Task<IActionResult> GetReceipt(Guid receiptId)
        => Ok(await Mediator.Send(new GetReceiptQuery(receiptId)));
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries.Records;
using SiradigCalc.Application.Queries.Forms;
using SiradigCalc.Application.Queries.Receipts;

namespace SiradigCalc.Api.Controllers.Query;

[Route("api/records/templates")]
[ApiController]
public class RecordTemplatesQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("forms/{id}")]
    public async Task<IActionResult> GetFormTemplate(Guid id)
        => Ok(await Mediator.Send(new GetFormTemplateQuery(id)));

    [HttpGet("forms")]
    public async Task<IActionResult> GetFormTemplates()
        => Ok(await Mediator.Send(new GetFormTemplatesQuery()));

    [HttpGet("receipts/{id}")]
    public async Task<IActionResult> GetReceiptTemplate(Guid id)
    => Ok(await Mediator.Send(new GetReceiptTemplateQuery(id)));

    [HttpGet("receipts")]
    public async Task<IActionResult> GetReceiptTemplates()
        => Ok(await Mediator.Send(new GetReceiptTemplatesQuery()));

    [HttpGet("links")]
    public async Task<IActionResult> GetReceiptTemplateLink([FromQuery] Guid receiptTemplateId, [FromQuery] Guid formTemplateId)
        => Ok(await Mediator.Send(new LinkTemplatesQuery(receiptTemplateId: receiptTemplateId, formTemplateId: formTemplateId)));

    [HttpGet("field-types")]
    public async Task<IActionResult> GetFieldTypes()
        => Ok(await Mediator.Send(new GetRecordFieldTypesQuery()));

}

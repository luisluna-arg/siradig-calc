using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries.DataContainers;
using SiradigCalc.Application.Queries.Receipts;

namespace SiradigCalc.Api.Controllers.Query;

[Route("api/receipts/templates")]
[ApiController]
public class ReceiptTemplatesQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReceiptTemplate(Guid id)
        => Ok(await Mediator.Send(new GetReceiptTemplateQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetReceiptTemplates()
        => Ok(await Mediator.Send(new GetReceiptTemplatesQuery()));

    [HttpGet("field-types")]
    public async Task<IActionResult> GetFieldTypes()
        => Ok(await Mediator.Send(new GetDataContainerFieldTypesQuery()));

    [HttpPost("{id}/links")]
    public async Task<IActionResult> GetReceiptTemplateLink(Guid id)
        => Ok(await Mediator.Send(new ReceiptLinksQuery(receiptTemplateId: id)));

    [HttpPost("{id}/links/{formTemplateId}")]
    public async Task<IActionResult> GetReceiptTemplateLink(Guid id, Guid formTemplateId)
        => Ok(await Mediator.Send(new LinkTemplatesQuery(receiptTemplateId: id, formTemplateId: formTemplateId)));

    [HttpPost("{id}/links/{formTemplateId}/{receiptFieldId}/links/{formFieldId}")]
    public async Task<IActionResult> GetReceiptTemplateLink(Guid id, Guid formTemplateId, Guid receiptFieldId, Guid formFieldId)
        => Ok(await Mediator.Send(new LinkFieldTemplatesQuery(
            receiptTemplateId: id,
            formTemplateId: formTemplateId,
            receiptFieldId: receiptFieldId,
            formFieldId: formFieldId)));
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Commands.Receipts;

namespace SiradigCalc.Api.Controllers;

[Route("api/receipts/templates")]
[ApiController]
public class ReceiptTemplatesCommandController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> CreateReceiptTemplate(CreateReceiptTemplateCommand command)
        => Ok(await Mediator.Send(command));

    [HttpPost("field")]
    public async Task<IActionResult> CreateReceiptFieldTemplate(CreateReceiptFieldCommand command)
        => Ok(await Mediator.Send(command));

    [HttpPost("{id}/link/{formTemplateId}")]
    public async Task<IActionResult> CreateReceiptTemplateLink(Guid id, Guid formTemplateId)
        => Ok(await Mediator.Send(new LinkTemplatesCommand(receiptTemplateId: id, formTemplateId: formTemplateId)));

    [HttpPost("{id}/link/{formTemplateId}/{receiptFieldId}/link/{formFieldId}")]
    public async Task<IActionResult> CreateReceiptTemplateLink(Guid id, Guid formTemplateId, Guid receiptFieldId, Guid formFieldId)
        => Ok(await Mediator.Send(new LinkFieldTemplatesCommand(
            receiptTemplateId: id, 
            formTemplateId: formTemplateId,
            receiptFieldId: receiptFieldId, 
            formFieldId: formFieldId)));
}

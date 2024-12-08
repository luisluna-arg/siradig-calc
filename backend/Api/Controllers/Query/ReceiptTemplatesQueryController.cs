using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Commands.DataContainers;
using SiradigCalc.Application.Queries.Receipts;

namespace SiradigCalc.Api.Controllers;

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
}

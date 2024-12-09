using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries.DataContainers;
using SiradigCalc.Application.Queries.Forms;

namespace SiradigCalc.Api.Controllers.Query;

[Route("api/forms/templates")]
[ApiController]
public class FormTemplatesQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetFormTemplate(Guid id)
        => Ok(await Mediator.Send(new GetFormTemplateQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetFormTemplates()
        => Ok(await Mediator.Send(new GetFormTemplatesQuery()));

    [HttpGet("field-types")]
    public async Task<IActionResult> GetFieldTypes()
        => Ok(await Mediator.Send(new GetDataContainerFieldTypesQuery()));
}

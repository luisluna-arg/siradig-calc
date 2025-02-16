using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries;

namespace SiradigCalc.Api.Controllers.Query;

[Route("api/records/templates")]
[ApiController]
public class RecordTemplatesQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecordTemplate(Guid id)
    => Ok(await Mediator.Send(new GetRecordTemplateQuery(id)));

    [HttpGet]
    public async Task<IActionResult> GetRecordTemplates()
        => Ok(await Mediator.Send(new GetRecordTemplatesQuery()));

    [HttpGet("links")]
    public async Task<IActionResult> GetRecordTemplateLinks()
        => Ok(await Mediator.Send(new GetRecordTemplateLinksQuery()));

    [HttpGet("links/{leftTemplateId}/to/{rightTemplateId}")]
    public async Task<IActionResult> GetRecordTemplateLink(Guid leftTemplateId, Guid rightTemplateId)
        => Ok(await Mediator.Send(new GetRecordTemplateLinkQuery(leftTemplateId, rightTemplateId)));

    [HttpGet("field-types")]
    public async Task<IActionResult> GetFieldTypes()
        => Ok(await Mediator.Send(new GetRecordFieldTypesQuery()));

}

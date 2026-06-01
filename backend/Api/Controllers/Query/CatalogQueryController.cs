using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries;

namespace SiradigCalc.Api.Controllers.Query;

[Route("api/catalog")]
[ApiController]
public class CatalogQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("records")]
    public async Task<IActionResult> GetRecords()
        => Ok(await Mediator.Send(new GetRecordCatalogQuery()));

    [HttpGet("templates")]
    public async Task<IActionResult> GetTemplates()
        => Ok(await Mediator.Send(new GetTemplateCatalogQuery()));

    [HttpGet("field-types")]
    public async Task<IActionResult> GetFieldTypes()
        => Ok(await Mediator.Send(new GetRecordFieldTypesCatalogQuery()));
}
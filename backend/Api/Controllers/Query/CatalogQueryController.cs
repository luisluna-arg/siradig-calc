using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries;

namespace SiradigCalc.Api.Controllers.Query;

[Route("api/catalog")]
[ApiController]
public class CatalogQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("templates")]
    public async Task<IActionResult> GetTemplates()
        => Ok(await Mediator.Send(new GetTemplateCatalogQuery()));
}
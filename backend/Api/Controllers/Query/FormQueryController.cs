using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Queries.Forms;

namespace SiradigCalc.Api.Controllers;

[Route("api/forms")]
[ApiController]
public class FormQueryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("{formId}")]
    public async Task<IActionResult> GetForm(Guid formId)
        => Ok(await Mediator.Send(new GetFormQuery(formId)));
    
    [HttpGet]
    public async Task<IActionResult> CreateForms()
        => Ok(await Mediator.Send(new GetFormsQuery()));
}

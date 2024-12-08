using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Commands.Forms;

namespace SiradigCalc.Api.Controllers;

[Route("api/forms")]
[ApiController]
public class FormCommandController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> CreateForm(CreateFormCommand command)
        => Ok(await Mediator.Send(command));

    [HttpPost("value")]
    public async Task<IActionResult> CreateFormValue(CreateFormValueCommand command)
        => Ok(await Mediator.Send(command));
}

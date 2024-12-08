using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Commands.Forms;

namespace SiradigCalc.Api.Controllers;

[Route("api/forms/templates")]
[ApiController]
public class FormTemplatesCommandController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> CreateFormTemplate(CreateFormTemplateCommand command)
        => Ok(await Mediator.Send(command));

    [HttpPost("field")]
    public async Task<IActionResult> CreateFormFieldTemplate(CreateFormFieldCommand command)
        => Ok(await Mediator.Send(command));
}

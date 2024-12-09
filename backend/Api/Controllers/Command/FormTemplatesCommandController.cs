using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Commands.Forms;

namespace SiradigCalc.Api.Controllers.Command;

[Route("api/forms/templates")]
[ApiController]
public class FormTemplatesCommandController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> CreateFormTemplate(CreateFormTemplateCommand command)
        => Ok(await Mediator.Send(command));

    [HttpDelete("{formTemplateId}")]
    public async Task<IActionResult> DeleteFormTemplate(Guid formTemplateId)
        => Ok(await Mediator.Send(new DeleteFormTemplateCommand(formTemplateId)));

    [HttpPost("field")]
    public async Task<IActionResult> CreateFormFieldTemplate(CreateFormFieldCommand command)
        => Ok(await Mediator.Send(command));

    [HttpDelete("field/{formFieldId}")]
    public async Task<IActionResult> DeleteFormFieldTemplate(Guid formFieldId)
        => Ok(await Mediator.Send(new DeleteFormFieldCommand(formFieldId)));
}

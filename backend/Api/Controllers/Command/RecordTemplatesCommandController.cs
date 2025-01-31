using MediatR;
using Microsoft.AspNetCore.Mvc;
using SiradigCalc.Api.Controllers.Base;
using SiradigCalc.Application.Commands;

namespace SiradigCalc.Api.Controllers.Command;

[Route("api/records/templates")]
[ApiController]
public class RecordTemplatesCommandController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> CreateRecordTemplate(CreateRecordTemplateCommand command)
        => Ok(await Mediator.Send(command));

    [HttpDelete("{recordTemplateId}")]
    public async Task<IActionResult> DeleteRecordTemplate(Guid recordTemplateId)
        => Ok(await Mediator.Send(new DeleteRecordTemplateCommand(recordTemplateId)));

    [HttpPost("field")]
    public async Task<IActionResult> CreateRecordTemplateFieldTemplate(CreateRecordTemplateFieldCommand command)
        => Ok(await Mediator.Send(command));

    [HttpDelete("field/{recordTemplateFieldId}")]
    public async Task<IActionResult> DeleteRecordTemplateFieldTemplate(Guid recordTemplateFieldId)
        => Ok(await Mediator.Send(new DeleteRecordTemplateFieldCommand(recordTemplateFieldId)));
}

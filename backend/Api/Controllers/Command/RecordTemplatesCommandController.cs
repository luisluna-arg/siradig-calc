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

    [HttpPut("{recordTemplateId}")]
    public async Task<IActionResult> UpdateRecordTemplate(Guid recordTemplateId, UpdateRecordTemplateCommand command)
    {
        command.Id = recordTemplateId;
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{recordTemplateId}")]
    public async Task<IActionResult> DeleteRecordTemplate(Guid recordTemplateId)
        => Ok(await Mediator.Send(new DeleteRecordTemplateCommand(recordTemplateId)));

    [HttpPost("{recordTemplateId}/section")]
    public async Task<IActionResult> CreateRecordTemplateSection(Guid recordTemplateId, CreateRecordTemplateSectionCommand command)
    {
        command.RecordTemplateId = recordTemplateId;
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("{recordTemplateId}/section/{recordTemplateSectionId}/field")]
    public async Task<IActionResult> CreateRecordTemplateField(Guid recordTemplateId, Guid recordTemplateSectionId, CreateRecordTemplateFieldCommand command)
    {
        command.RecordTemplateId = recordTemplateId;
        command.RecordTemplateSectionId = recordTemplateSectionId;
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("field/{recordTemplateFieldId}")]
    public async Task<IActionResult> DeleteRecordTemplateField(Guid recordTemplateFieldId)
        => Ok(await Mediator.Send(new DeleteRecordTemplateFieldCommand(recordTemplateFieldId)));

    [HttpPost("link/{leftTemplateId}/to/{rightTemplateId}")]
    public async Task<IActionResult> CreateRecordTemplateLink(Guid leftTemplateId, Guid rightTemplateId, LinkTemplatesCommand command)
    {
        command.LeftTemplateId = leftTemplateId;
        command.RightTemplateId = rightTemplateId;
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("link/{leftTemplateId}/to/{rightTemplateId}")]
    public async Task<IActionResult> DeleteRecordTemplateLink(Guid leftTemplateId, Guid rightTemplateId)
        => Ok(await Mediator.Send(new UnlinkTemplatesCommand(leftTemplateId: leftTemplateId, rightTemplateId: rightTemplateId)));

    [HttpPost("link/{leftTemplateId}/to/{rightTemplateId}/{leftFieldId}/to/{rightFieldId}")]
    public async Task<IActionResult> CreateRecordTemplateLink(Guid leftTemplateId, Guid rightTemplateId, Guid rightFieldId, Guid leftFieldId)
        => Ok(await Mediator.Send(new LinkFieldTemplatesCommand(
            leftTemplateId: leftTemplateId,
            rightTemplateId: rightTemplateId,
            rightFieldId: rightFieldId,
            leftFieldId: leftFieldId)));

    [HttpDelete("link/{leftTemplateId}/to/{rightTemplateId}/{leftFieldId}/to/{rightFieldId}")]
    public async Task<IActionResult> DeleteRecordTemplateLink(Guid leftTemplateId, Guid rightTemplateId, Guid rightFieldId, Guid leftFieldId)
        => Ok(await Mediator.Send(new UnlinkFieldTemplatesCommand(
            leftTemplateId: leftTemplateId,
            rightTemplateId: rightTemplateId,
            rightFieldId: rightFieldId,
            leftFieldId: leftFieldId)));
}

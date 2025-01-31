using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class LinkFieldTemplatesCommandValidator : AbstractValidator<LinkFieldTemplatesCommand>
{
    public LinkFieldTemplatesCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.LeftTemplateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("LeftTemplateId is required.")
            .MustAsync(async (leftTemplateId, cancellationToken) =>
                await dbContext.RecordTemplates.AnyAsync(rt => rt.Id == leftTemplateId))
            .WithMessage("The provided left RecordTemplateId does not exist in the database.");

        RuleFor(c => c.RightTemplateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("RightTemplateId is required.")
            .MustAsync(async (rightTemplateId, cancellationToken) =>
                await dbContext.RecordTemplates.AnyAsync(ft => ft.Id == rightTemplateId))
            .WithMessage("The provided right RecordTemplateId does not exist in the database.");

        RuleFor(c => c.LeftFieldId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("LeftFieldId is required.")
            .MustAsync(async (leftFieldId, cancellationToken) =>
                await dbContext.RecordTemplateFields.AnyAsync(rf => rf.Id == leftFieldId))
            .WithMessage("The provided left RecordTemplateFieldId does not exist in the database.");

        RuleFor(c => c.RightFieldId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("RightFieldId is required.")
            .MustAsync(async (rightFieldId, cancellationToken) =>
                await dbContext.RecordTemplateFields.AnyAsync(ff => ff.Id == rightFieldId))
            .WithMessage("The provided right RecordTemplateFieldId does not exist in the database.");

    }
}
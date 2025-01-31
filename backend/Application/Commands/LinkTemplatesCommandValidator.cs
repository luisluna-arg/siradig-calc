using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class LinkTemplatesCommandValidator : AbstractValidator<LinkTemplatesCommand>
{
    public LinkTemplatesCommandValidator(ISolutionDbContext dbContext)
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
    }
}
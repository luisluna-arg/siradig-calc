using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public abstract class CreateRecordCommandValidator : AbstractValidator<Record>
{
    private const short TITLE_SIZE = 100;

    public CreateRecordCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage($"Title must not exceed {TITLE_SIZE} characters.");

        RuleFor(c => c.TemplateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("TemplateId is required.")
            .MustAsync(async (templateId, cancellationToken) =>
                await dbContext.RecordTemplates.AnyAsync(t => t.Id == templateId))
            .WithMessage("The provided TemplateId does not exist in the database.");

        RuleFor(c => c.Values)
            .NotNull()
            .WithMessage("Values cannot be null.")
            .Must(values => values.Count > 0)
            .WithMessage("Values must contain at least one item.")
            .ForEach(valueRule => valueRule.NotNull().WithMessage("Each value must not be null."));
    }
}

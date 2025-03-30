using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UpdateRecordCommandValidator
    : AbstractValidator<UpdateRecordCommand>
{
    private const short TITLE_SIZE = 100;

    public UpdateRecordCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage($"Name must not exceed {TITLE_SIZE} characters.");

        RuleFor(c => c.TemplateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("TemplateId is required.")
            .MustAsync(async (templateId, cancellationToken) =>
                await dbContext.RecordTemplates.AnyAsync(t => t.Id.Equals(templateId)))
            .WithMessage("The provided TemplateId does not exist.");
    }
}

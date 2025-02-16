using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class CreateRecordTemplateFieldCommandValidator : AbstractValidator<CreateRecordTemplateFieldCommand>
{
    public CreateRecordTemplateFieldCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (c, cancel) => await dbContext.RecordTemplates.AnyAsync(o => o.Id == c.RecordTemplateId, cancel))
            .WithMessage($"Record template must exist.")
            .MustAsync(async (c, cancel) => await dbContext.RecordTemplateSections.AnyAsync(o => o.Id == c.RecordTemplateSectionId && c.RecordTemplateId == c.RecordTemplateId, cancel))
            .WithMessage($"Record template section must exist.");

        RuleFor(c => c.Label)
            .NotEmpty()
            .WithMessage("Label is required.");
    }
}

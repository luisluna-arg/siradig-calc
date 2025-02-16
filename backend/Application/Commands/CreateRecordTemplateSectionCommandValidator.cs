using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class CreateRecordTemplateSectionCommandValidator : AbstractValidator<CreateRecordTemplateSectionCommand>
{
    public CreateRecordTemplateSectionCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (c, cancel) => await dbContext.RecordTemplates.AnyAsync(o => o.Id == c.RecordTemplateId, cancel))
            .WithMessage($"Record template must exist.");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
    }
}

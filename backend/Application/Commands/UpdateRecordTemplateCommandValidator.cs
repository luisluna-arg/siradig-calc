using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UpdateRecordTemplateCommandValidator : AbstractValidator<UpdateRecordTemplateCommand>
{
    private const short NAME_SIZE = 100;
    private const short DESCRIPTION_SIZE = 200;

    public UpdateRecordTemplateCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Template Id is required.")
            .MustAsync(async (templateId, cancellationToken) =>
                await dbContext.RecordTemplates.AnyAsync(t => t.Id.Equals(templateId)))
            .WithMessage("The provided TemplateId does not exist.");

        RuleFor(c => c.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(NAME_SIZE)
            .WithMessage($"Name must not exceed {NAME_SIZE} characters.");

        RuleFor(c => c.Description)
            .MaximumLength(DESCRIPTION_SIZE)
            .WithMessage($"Description must not exceed {DESCRIPTION_SIZE} characters.");

        RuleFor(c => c.Sections)
            .NotNull()
            .WithMessage("Sections cannot be null.")
            .Must(sections => sections.Count > 0)
            .WithMessage("Sections must contain at least one item.")
            .ForEach(section => section.SetValidator(new UpdateSectionValidator<UpdateFieldDtoValidator>(dbContext)));
    }
}

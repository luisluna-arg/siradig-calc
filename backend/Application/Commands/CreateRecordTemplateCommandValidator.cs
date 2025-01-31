using FluentValidation;

namespace SiradigCalc.Application.Commands;

public abstract class CreateRecordTemplateCommandValidator : AbstractValidator<CreateRecordTemplateCommand>
{
    private const short NAME_SIZE = 100;
    private const short DESCRIPTION_SIZE = 200;

    public CreateRecordTemplateCommandValidator()
    {
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
            .ForEach(section => section.SetValidator(new CreateSectionValidator<CreateFieldDtoValidator>()));
    }
}

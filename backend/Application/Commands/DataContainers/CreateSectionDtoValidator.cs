using FluentValidation;
using SiradigCalc.Application.Dtos;

namespace SiradigCalc.Application.Commands.DataContainers;

public class CreateSectionDtoValidator<FieldDtoValidator> : AbstractValidator<CreateSectionDto>
    where FieldDtoValidator : CreateFieldDtoValidator, new()
{
    private const short NAME_SIZE = 100;

    public CreateSectionDtoValidator()
    {
        RuleFor(s => s.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Section name is required.")
            .MaximumLength(100)
            .WithMessage($"Section name must not exceed {NAME_SIZE} characters.");

        RuleFor(s => s.Fields)
            .NotNull()
            .WithMessage("Fields cannot be null.")
            .Must(fields => fields.Any())
            .WithMessage("Fields must contain at least one item.")
            .ForEach(field => field.SetValidator(new FieldDtoValidator()));
    }
}

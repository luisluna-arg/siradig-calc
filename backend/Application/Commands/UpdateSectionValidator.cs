using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Application.Dtos;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UpdateSectionValidator<FieldDtoValidator> : AbstractValidator<UpdateSectionDto>
{
    private const short NAME_SIZE = 100;

    public UpdateSectionValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.Id)
            .MustAsync(async (sectionId, cancellationToken) =>
                await dbContext.RecordTemplateSections.AnyAsync(t => t.Id.Equals(sectionId!.Value)))
            .WithMessage("The provided Section Id does not exist.")
            .When(c => c.Id.HasValue);

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
            .ForEach(field => field.SetValidator(new UpdateFieldDtoValidator(dbContext)));
    }
}

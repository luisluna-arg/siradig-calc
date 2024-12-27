using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.DataContainers;

public abstract class CreateDataContainerValueCommandValidator<TField, TValue> : AbstractValidator<CreateDataContainerValueCommand>
    where TField : BaseDataContainerField, new()
    where TValue : BaseDataContainerValue<TField>, new()
{
    private const short NAME_SIZE = 100;

    public CreateDataContainerValueCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.FieldId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("FieldId is required.")
            .MustAsync(async (fieldId, cancellationToken) =>
                await dbContext.Set<TValue>().AnyAsync(f => f.Id == fieldId, cancellationToken))
            .WithMessage("The provided FieldId does not exist in the database.");

        RuleFor(c => c.Value)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Value is required.")
            .MaximumLength(100)
            .WithMessage($"Value must not exceed {NAME_SIZE} characters.");
    }
}

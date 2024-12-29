using FluentValidation;

namespace SiradigCalc.Application.Commands.Records;

public class DeleteInstanceCommandValidator<TId> : AbstractValidator<DeleteInstanceCommand<TId>>
{
    public DeleteInstanceCommandValidator()
    {
        RuleFor(c => c.Id)
            .Must(id => id!.Equals(default(TId)!))
            .WithMessage("If provided, Id must not be the default value.")
            .When(c => c.Id != null);
    }
}

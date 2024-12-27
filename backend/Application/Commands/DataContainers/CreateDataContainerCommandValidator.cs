using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.DataContainers;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.DataContainers;

public abstract class CreateDataContainerCommandValidator<TCommand, TValueDto, TDataContainerTemplate, TDataContainerSection, TField>
    : AbstractValidator<TCommand>
    where TCommand : CreateDataContainerCommand<TValueDto>
    where TField : BaseDataContainerField, new()
    where TDataContainerSection : BaseDataContainerSection<TField>, new()
    where TDataContainerTemplate : BaseDataContainer<TDataContainerSection, TField>, new()
{
    private const short NAME_SIZE = 100;
    private const short DESCRIPTION_SIZE = 200;

    public CreateDataContainerCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage($"Name must not exceed {NAME_SIZE} characters.");

        RuleFor(c => c.Description)
            .MaximumLength(100)
            .WithMessage($"Description must not exceed {DESCRIPTION_SIZE} characters.");

        RuleFor(c => c.TemplateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("TemplateId is required.")
            .MustAsync(async (templateId, cancellationToken) =>
                await dbContext.Set<TDataContainerTemplate>().AnyAsync(t => t.Id == templateId))
            .WithMessage("The provided TemplateId does not exist in the database.");

        RuleFor(c => c.Values)
            .NotNull()
            .WithMessage("Values cannot be null.")
            .Must(values => values.Count > 0)
            .WithMessage("Values must contain at least one item.")
            .ForEach(valueRule => valueRule.NotNull().WithMessage("Each value must not be null."));
    }
}

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Core.Entities.Base.Records;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Records;

public abstract class UpdateRecordCommandValidator<TCommand, TRecordTemplate, TRecordSection, TField>
    : AbstractValidator<TCommand>
    where TCommand : UpdateRecordCommand<Guid, Guid>
    where TField : BaseRecordField, new()
    where TRecordSection : BaseRecordSection<TField>, new()
    where TRecordTemplate : BaseRecordTemplate<TRecordSection, TField>, new()
{
    private const short NAME_SIZE = 100;
    private const short DESCRIPTION_SIZE = 200;

    public UpdateRecordCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.Title)
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
                await dbContext.Set<TRecordTemplate>().AnyAsync(t => t.Id.Equals(templateId)))
            .WithMessage("The provided TemplateId does not exist in the database.");
    }
}

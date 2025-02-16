using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class DeleteAllRecordConversionsCommandValidator : AbstractValidator<DeleteAllRecordConversionsCommand>
{
    public DeleteAllRecordConversionsCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.SourceId)
            .NotEmpty()
            .MustAsync(async (sourceId, cancel) => await dbContext.Records
                .AnyAsync(o => o.Id == sourceId, cancel))
            .WithMessage("Source record needs to exist.");
    }
}

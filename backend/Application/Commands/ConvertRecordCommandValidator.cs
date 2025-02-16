using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class ConvertRecordCommandValidator : AbstractValidator<ConvertRecordCommand>
{
    public ConvertRecordCommandValidator(ISolutionDbContext dbContext)
    {
        RuleFor(c => c.SourceId)
            .NotEmpty()
            .MustAsync(async (sourceId, cancel) => await dbContext.Records
                .AnyAsync(
                    o => o.Id == sourceId, cancel))
            .WithMessage("Source record needs to exist.");
    
        
        RuleFor(c => c.TargetTemplateId)
            .NotEmpty()
            .MustAsync(async (targetTemplateId, cancel) => await dbContext.RecordTemplates
                .AnyAsync(
                    o => o.Id == targetTemplateId, cancel))
            .WithMessage("Target template needs to exist.");
    }
}

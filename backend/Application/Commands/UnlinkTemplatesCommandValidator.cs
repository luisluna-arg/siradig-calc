using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UnlinkTemplatesCommandValidator(ISolutionDbContext dbContext) : LinkTemplatesCommandValidator(dbContext)
{
}
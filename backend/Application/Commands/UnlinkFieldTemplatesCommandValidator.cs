using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands;

public class UnlinkFieldTemplatesCommandValidator(ISolutionDbContext dbContext) : LinkFieldTemplatesCommandValidator(dbContext)
{
}
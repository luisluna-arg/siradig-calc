using SiradigCalc.Application.Commands.Records;
using SiradigCalc.Core.Entities.Forms;
using SiradigCalc.Infra.Persistence.DbContexts;

namespace SiradigCalc.Application.Commands.Forms;

public class CreateFormValueCommand : CreateRecordValueCommand
{
}

public class CreateFormValueHandler(ISolutionDbContext dbContext)
    : CreateRecordValueCommandHandler<CreateFormValueCommand, FormField, FormValue>(dbContext)
{
}

public class CreateFormValueCommandValidator(ISolutionDbContext dbContext)
    : CreateRecordValueCommandValidator<FormField, FormValue>(dbContext)
{
}
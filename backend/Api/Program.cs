using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using SiradigCalc.Api.Common;
using SiradigCalc.ApiFramework.Core.Config;
using SiradigCalc.Application.Validation;
using SiradigCalc.Infra.Persistence.DbContexts;
using SiradigCalc.Application.Queries;
using SiradigCalc.ApiFramework.Config;

var builder = WebApplication.CreateBuilder(args);

var programAssembly = typeof(Program).Assembly;
var applicationAssembly = typeof(GetRecordQuery).Assembly;

builder.Services.AddControllers();
builder.Services.AddOpenApi(Constants.OPENAPI_VERSION_LABEL);
builder.Services.AddMediatR(programAssembly);
builder.Services.AddMediatR(applicationAssembly);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddValidatorsFromAssemblyContaining<GetRecordQuery>();
builder.Services.AddRecordConverters();
builder.Services.AddParsers();
builder.Services.AddDtoMappers();
builder.Services.AddPdfReceiptParsing();
builder.Services.EnableCors();

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole().AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
});

builder.Services
    .AddDbContext<ISolutionDbContext, SolutionDbContext>(opt =>
        opt.UseLazyLoadingProxies(false)
            .UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb"))
            .UseLoggerFactory(loggerFactory)
            .EnableSensitiveDataLogging());

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors(Policies.AllowAll);

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ISolutionDbContext>();
    dbContext.Database.Migrate();
}

app.Run();

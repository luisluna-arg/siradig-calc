using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.OpenApi.Models;
using SiradigCalc.Api.Common;
using SiradigCalc.ApiFramework.Core.Config;
using SiradigCalc.Application.Validation;
using SiradigCalc.Infra.Persistence.DbContexts;
using System.Text.Json;
using Swashbuckle.AspNetCore.Swagger;
using SiradigCalc.Application.Queries;
using SiradigCalc.ApiFramework.Config;

var builder = WebApplication.CreateBuilder(args);

var programAssembly = typeof(Program).Assembly;
var applicationAssembly = typeof(GetRecordQuery).Assembly;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(programAssembly);
builder.Services.AddMediatR(applicationAssembly);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddValidatorsFromAssemblyContaining<GetRecordQuery>();
builder.Services.AddRecordConverters();
builder.Services.AddParsers();
builder.Services.AddDtoMappers();
builder.Services.EnableCors();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(Constants.OPENAPI_VERSION_LABEL, new OpenApiInfo
    {
        Title = Constants.OPEN_API_TITLE,
        Version = Constants.OPENAPI_VERSION
    });
});

builder.Services
    .AddDbContext<ISolutionDbContext, SolutionDbContext>(opt =>
        opt.UseLazyLoadingProxies(false)
            .UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb")));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "OpenApi", Constants.OPENAPI_SPEC_FILE_NAME);
    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

    var swaggerProvider = app.Services.GetRequiredService<ISwaggerProvider>();
    var swaggerDoc = swaggerProvider.GetSwagger(Constants.OPENAPI_VERSION_LABEL);

    File.WriteAllText(filePath, JsonSerializer.Serialize(swaggerDoc, new JsonSerializerOptions
    {
        WriteIndented = true
    }));

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/{Constants.OPENAPI_VERSION_LABEL}/{Constants.SWAGGER_FILE_NAME}", Constants.OPEN_API_TITLE);
    });
}

app.UseHttpsRedirection();

app.UseCors(Policies.AllowAll);

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ISolutionDbContext>();
    var databaseCreator = dbContext.Database.GetService<IRelationalDatabaseCreator>();
    if (!databaseCreator.Exists())
    {
        dbContext.Database.Migrate();
    }
}

app.Run();
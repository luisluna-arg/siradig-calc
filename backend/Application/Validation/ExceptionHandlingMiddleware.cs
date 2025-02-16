using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace SiradigCalc.Application.Validation;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                Message = "Validation failed.",
                Errors = ex.Errors.Select(e => new { property = e.PropertyName, error = e.ErrorMessage })
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "html/text";

            var errors = new List<string>() { ex.Message };

            var currentEx = ex;
            while (currentEx.InnerException != null)
            {
                currentEx = currentEx.InnerException;
                errors.Add(currentEx.Message);
            }

            var errorResponse = new
            {
                Message = "An unexpected error occurred.",
                Errors = errors
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}

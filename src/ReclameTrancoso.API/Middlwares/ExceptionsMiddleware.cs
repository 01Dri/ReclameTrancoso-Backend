using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using ReclameTrancoso.Exceptions.Exceptions;

namespace API.Middlwares;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        Dictionary<Type, int> exceptionStatusCodes = new Dictionary<Type, int>()
        {
            { typeof(NotFoundException), StatusCodes.Status404NotFound },
            { typeof(InvalidPasswordException), StatusCodes.Status400BadRequest },
        };
        
        if (exception is ValidationException)
        {
            var validationException = (ValidationException) exception;
            return FluentValidationErrorsResponse(context, validationException.Errors);
        }
        
        var statusCode = StatusCodes.Status500InternalServerError; // Internal Server Error por padrão
        if (exceptionStatusCodes.ContainsKey(exception.GetType()))
        {
            statusCode = exceptionStatusCodes[exception.GetType()];
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        return context.Response.WriteAsync(result);
    }

    private static async Task FluentValidationErrorsResponse(HttpContext context, IEnumerable<ValidationFailure> failures)
    {
        var errors = failures.Select(err => new
        {
            Field = err.PropertyName,
            Error = err.ErrorMessage
        });

        var errorResponse = new
        {
            Message = "Validation Failed",
            Errors = errors
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;


        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
    }
}
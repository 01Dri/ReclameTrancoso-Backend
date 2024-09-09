using FluentValidation;
using Newtonsoft.Json;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var errors = ex.Errors.Select(err => new
            {
                Field = err.PropertyName,
                Error = err.ErrorMessage
            });

            var errorResponse = new
            {
                Message = "Validation Failed",
                Errors = errors
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}
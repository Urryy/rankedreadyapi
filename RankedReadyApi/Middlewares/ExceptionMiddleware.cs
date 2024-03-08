using System.ComponentModel.DataAnnotations;
using System.Net;

namespace RankedReadyApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ArgumentOutOfRangeException exc)
        {
            _logger.LogError(exc.Message);
            await HandleExceptionAsync(httpContext, exc);
        }
        catch (FieldAccessException exc)
        {
            _logger.LogError(exc.Message);
            await HandleExceptionAsync(httpContext, exc);
        }
        catch (InvalidDataException exc)
        {
            _logger.LogError(exc.Message);
            await HandleExceptionAsync(httpContext, exc);
        }
        catch (AccessViolationException exc)
        {
            _logger.LogError(exc.Message);
            await HandleExceptionAsync(httpContext, exc);
        }
        catch (ValidationException exc)
        {
            _logger.LogError(exc.Message);
            await HandleExceptionAsync(httpContext, exc);
        }
        catch (NullReferenceException exc)
        {
            _logger.LogError(exc.Message);
            await HandleExceptionAsync(httpContext, exc);
        }
        catch (ArgumentNullException exc)
        {
            _logger.LogError(exc.Message);
            await HandleExceptionAsync(httpContext, exc);
        }
        catch (ArgumentException exc)
        {
            _logger.LogError(exc.Message);
            await HandleExceptionAsync(httpContext, exc);
        }
        catch (Exception exc)
        {
            _logger.LogError(exc.Message);
            await HandleExceptionAsync(httpContext, exc);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var message = $"CatchedException: {ex.Message}.";
        await context.Response.WriteAsync(message);
    }
}

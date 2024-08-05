using Minio.Exceptions;
using StateTaxService.AIAT.Common.Enums;
using StateTaxService.AIAT.Common.Exceptions;
using StateTaxService.AIAT.Common.Models.Responses;
using System.Net;
using System.Text.Json;

namespace StateTaxService.AIAT.Inventory.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(RequestDelegate next,
                            ILogger<ExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (STSNotFoundException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.NotFound);
        }
        catch (STSValidationErrorException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (ArgumentNullException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (STSForbiddenException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.Forbidden);
        }
        catch (ForbiddenException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.Forbidden);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.Unauthorized);
        }
        catch (OperationCanceledException ex)
        {
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new ResponseModel<object>();

        switch (statusCode)
        {
            case HttpStatusCode.NotFound:
            case HttpStatusCode.BadRequest:
                response = ResponseModel<object>.Error(message, ResponseSeverity.Warning);
                break;

            default:
                response = ResponseModel<object>.Error(message, ResponseSeverity.Error);
                break;
        }

        var jsonResponse = JsonSerializer.Serialize(
                    response,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        await context.Response.WriteAsync(jsonResponse);
    }
}

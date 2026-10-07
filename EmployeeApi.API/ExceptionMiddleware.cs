using System;
using System.Net;
using System.Text.Json;

namespace EmployeeApi.API;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            await WriteError(
                context,
                HttpStatusCode.NotFound,
                ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await WriteError(
                context,
                HttpStatusCode.Conflict,
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unhandled exception");

            await WriteError(
                context,
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    private static async Task WriteError(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode =
            (int)statusCode;

        context.Response.ContentType =
            "application/problem+json";

        var response = new
        {
            type = "about:blank",
            title = statusCode.ToString(),
            status = (int)statusCode,
            detail = message,
            traceId = context.TraceIdentifier
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}
using System.Net;
using System.Text.Json;

namespace Challenge_ABM_Clientes.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log del error
            _logger.LogError(ex, "Error no controlado");

            // Respuesta estándar
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = context.Response.StatusCode,
                message = "Ocurrió un error inesperado"
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
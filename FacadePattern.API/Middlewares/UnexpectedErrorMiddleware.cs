using FacadePattern.API.Settings.NotificationHandlerSettings;
using System.Text.Json;

namespace FacadePattern.API.Middlewares;

public sealed class UnexpectedErrorMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new List<Notification>()
            {
                new()
                {
                    Key = "Unexpected Error",
                    Message = "An unexpected error happened."
                }
            };

            var responseJsonString = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(responseJsonString);
        }
    }
}

using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Middlewares;
public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerService _loggerService;
    public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
        _next = next;
        _loggerService = loggerService;
    }
    public async Task Invoke(HttpContext context)
    {
        var time = Stopwatch.StartNew();
        try
        {

            string message = "[Request] HTTP" + context.Request.Method + "-" + context.Request.Path;
            _loggerService.Write(message);
            await _next(context);
            time.Stop();
            message = "[Response] HTTP" + context.Request.Method + " - " + context.Request.Path + " - " + context.Response.StatusCode + " in " + time.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);
        }
        catch (Exception ex)
        {

            time.Stop();
            await HandleException(ex, context, time);
        }



    }

    private Task HandleException(Exception ex, HttpContext context, Stopwatch time)
    {
        string error = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " - error message" + ex.Message + " in " + time.Elapsed.TotalMilliseconds + " ms";
        _loggerService.Write(error);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

        return context.Response.WriteAsync(result);

    }
}
public static class CustomExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}

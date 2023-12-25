using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class GlobalLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalLoggingMiddleware> _logger;

    public GlobalLoggingMiddleware(RequestDelegate next, ILogger<GlobalLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // Log the request information
        var originalRequestBody = await LogRequest(context.Request);

        // Call the next middleware in the pipeline
        await _next(context);

        // Restore the original request body for downstream components
        context.Request.Body = originalRequestBody;

        // Log the response information
        LogResponse(context.Response);
    }

    private async Task<Stream> LogRequest(HttpRequest request)
    {
        var requestBodyStream = new MemoryStream();
        var originalRequestBody = request.Body; // Save the original request body

        await request.Body.CopyToAsync(requestBodyStream);
        requestBodyStream.Seek(0, SeekOrigin.Begin);
        var requestBody = await new StreamReader(requestBodyStream).ReadToEndAsync();

        _logger.LogInformation($"Request: {request.Method} {request.Path} {request.QueryString} {requestBody}");

        // Restore the original request body
        request.Body = originalRequestBody;

        return requestBodyStream;
    }



    private void LogResponse(HttpResponse response)
    {
        // Log the response status code and headers
        _logger.LogInformation($"Response: {response.StatusCode}");
    }
}

public static class GlobalLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalLoggingMiddleware>();
    }
}

using System.Net;
using System.Text.Json;
using ECommercePlatform.Core.DTOs;

namespace ECommercePlatform.API.Startup.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                var (response, statusCode) = HandleException(ex);
                
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
            }
        }

        private (ApiResponse response, int statusCode) HandleException(Exception ex)
        {
            return ex switch
            {
                KeyNotFoundException => (ApiResponse.ErrorResult(new List<string> { "Resource not found" }), 404),
                ArgumentException => (ApiResponse.ErrorResult(new List<string> { ex.Message }), 400),
                UnauthorizedAccessException => (ApiResponse.ErrorResult(new List<string> { "Unauthorized access" }), 401),
                InvalidOperationException => (ApiResponse.ErrorResult(new List<string> { ex.Message }), 409),
                _ => (ApiResponse.ErrorResult(new List<string> { "An unexpected error occurred" }), 500)
            };
        }
    }
}



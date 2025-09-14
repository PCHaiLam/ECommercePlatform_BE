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

                var response = HandleException(ex);
                
                context.Response.StatusCode = response.StatusCode;
                context.Response.ContentType = "application/json";

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
            }
        }

        private ApiResponse HandleException(Exception ex)
        {
            return ex switch
            {
                KeyNotFoundException => ApiResponse.ErrorResult("Resource not found", 404),
                ArgumentException => ApiResponse.ErrorResult(ex.Message, 400),
                UnauthorizedAccessException => ApiResponse.ErrorResult("Unauthorized access", 401),
                InvalidOperationException => ApiResponse.ErrorResult(ex.Message, 409),
                _ => ApiResponse.ErrorResult("An unexpected error occurred", 500)
            };
        }
    }
}



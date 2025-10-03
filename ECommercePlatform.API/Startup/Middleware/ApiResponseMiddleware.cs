using System.Text;
using System.Text.Json;
using ECommercePlatform.Core.DTOs;
using ECommercePlatform.Core.Exceptions;

namespace ECommercePlatform.API.Startup.Middleware
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiResponseMiddleware> _logger;

        public ApiResponseMiddleware(RequestDelegate next, ILogger<ApiResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api"))
            {
                await _next(context);
                return;
            }

            var originalBodyStream = context.Response.Body;

            try
            {
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                await _next(context);

                await WrapSuccessResponse(context, responseBody, originalBodyStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionResponse(context, ex, originalBodyStream);
            }
        }

        private async Task WrapSuccessResponse(HttpContext context, MemoryStream responseBody, Stream originalBodyStream)
        {
            context.Response.Body = originalBodyStream;

            responseBody.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBody).ReadToEndAsync();

            if (IsAlreadyApiResponse(responseText))
            {
                await context.Response.WriteAsync(responseText);
                return;
            }

            var statusCode = context.Response.StatusCode;
            
            // Handle error responses (validation errors, etc.)
            if (statusCode >= 400)
            {
                await HandleErrorResponse(context, responseText, statusCode, originalBodyStream);
                return;
            }

            // Handle success responses
            object? data = null;
            if (!string.IsNullOrEmpty(responseText))
            {
                try
                {
                    data = JsonSerializer.Deserialize<object>(responseText);
                }
                catch
                {
                    data = responseText;
                }
            }

            var apiResponse = ApiResponse<object>.Ok(data ?? new object());
            var wrappedJson = JsonSerializer.Serialize(apiResponse, GetJsonOptions());
            
            context.Response.ContentType = "application/json";
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(wrappedJson);
            
            await context.Response.WriteAsync(wrappedJson);
        }

        private async Task HandleExceptionResponse(HttpContext context, Exception ex, Stream originalBodyStream)
        {
            context.Response.Body = originalBodyStream;

            var (response, statusCode) = ex switch
            {
                NotFoundException nf => (ApiResponse.Error(nf.ErrorCode, nf.Message), 404),
                ValidationException ve => (ApiResponse.Error(ve.ErrorCode, ve.Message), 400),
                BusinessException be => (ApiResponse.Error(be.ErrorCode, be.Message), 400),
                UnauthorizedException ue => (ApiResponse.Error(ue.ErrorCode, ue.Message), 401),
                KeyNotFoundException => (ApiResponse.Error("NOT_FOUND", "Resource not found"), 404),
                ArgumentException => (ApiResponse.Error("INVALID_ARGUMENT", ex.Message), 400),
                UnauthorizedAccessException => (ApiResponse.Error("UNAUTHORIZED", "Unauthorized access"), 401),
                InvalidOperationException => (ApiResponse.Error("INVALID_OPERATION", ex.Message), 409),
                _ => (ApiResponse.Error("INTERNAL_ERROR", "An unexpected error occurred"), 500)
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var jsonResponse = JsonSerializer.Serialize(response, GetJsonOptions());
            await context.Response.WriteAsync(jsonResponse);
        }

        private bool IsAlreadyApiResponse(string responseText)
        {
            if (string.IsNullOrEmpty(responseText)) return false;
            
            try
            {
                var json = JsonDocument.Parse(responseText);
                return json.RootElement.TryGetProperty("success", out _);
            }
            catch
            {
                return false;
            }
        }

        private async Task HandleErrorResponse(HttpContext context, string responseText, int statusCode, Stream originalBodyStream)
        {
            context.Response.Body = originalBodyStream;

            var (errorCode, message) = GetErrorInfo(statusCode, responseText);
            var apiResponse = ApiResponse.Error(errorCode, message);
            
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var jsonResponse = JsonSerializer.Serialize(apiResponse, GetJsonOptions());
            await context.Response.WriteAsync(jsonResponse);
        }

        private (string errorCode, string message) GetErrorInfo(int statusCode, string responseText)
        {
            try
            {
                if (!string.IsNullOrEmpty(responseText))
                {
                    var json = JsonDocument.Parse(responseText);
                    
                    // Handle ASP.NET Core validation errors
                    if (json.RootElement.TryGetProperty("errors", out var errorsElement))
                    {
                        var errorMessages = new List<string>();
                        foreach (var error in errorsElement.EnumerateObject())
                        {
                            foreach (var message in error.Value.EnumerateArray())
                            {
                                var msg = message.GetString();
                                return ("VALIDATION_ERROR", msg ?? "Validation error");
                            }
                        }
                        
                    }
                    
                    // Handle other error formats
                    if (json.RootElement.TryGetProperty("title", out var titleElement))
                    {
                        var title = titleElement.GetString();
                        if (!string.IsNullOrEmpty(title) && title != "One or more validation errors occurred.")
                            return (GetErrorCodeFromStatus(statusCode), title);
                    }
                }
            }
            catch
            {
                // If parsing fails, fall back to default
            }

            return statusCode switch
            {
                400 => ("VALIDATION_ERROR", "Validation failed"),
                401 => ("UNAUTHORIZED", "Unauthorized access"),
                403 => ("FORBIDDEN", "Access forbidden"),
                404 => ("NOT_FOUND", "Resource not found"),
                409 => ("CONFLICT", "Conflict occurred"),
                422 => ("VALIDATION_ERROR", "Validation failed"),
                _ => ("ERROR", "An error occurred")
            };
        }

        private string GetErrorCodeFromStatus(int statusCode) => statusCode switch
        {
            400 => "BAD_REQUEST",
            401 => "UNAUTHORIZED", 
            403 => "FORBIDDEN",
            404 => "NOT_FOUND",
            409 => "CONFLICT",
            422 => "VALIDATION_ERROR",
            _ => "ERROR"
        };

        private JsonSerializerOptions GetJsonOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }
    }
}
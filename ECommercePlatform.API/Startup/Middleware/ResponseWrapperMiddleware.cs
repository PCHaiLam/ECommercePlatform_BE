using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ECommercePlatform.Core.DTOs;
using ECommercePlatform.Core.Exceptions;

namespace ECommercePlatform.API.Startup.Middleware
{
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseWrapperMiddleware> _logger;

        public ResponseWrapperMiddleware(RequestDelegate next, ILogger<ResponseWrapperMiddleware> logger)
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

                await WrapResponse(context, responseBody, originalBodyStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during request processing");
                await HandleExceptionAsync(context, ex, originalBodyStream);
            }
        }

        private async Task WrapResponse(HttpContext context, MemoryStream responseBody, 
            Stream originalBodyStream)
        {
            context.Response.Body = originalBodyStream;
            
            var statusCode = context.Response.StatusCode;
            var isSuccess = statusCode >= 200 && statusCode < 300;

            responseBody.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBody).ReadToEndAsync();

            object wrappedResponse;

            // Skip wrapping nếu đã là ApiResponse
            if (IsAlreadyApiResponse(responseText))
            {
                await context.Response.WriteAsync(responseText);
                return;
            }

            if (isSuccess)
            {
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

                wrappedResponse = new ApiResponse<object>
                {
                    Success = true,
                    Data = data,
                    Message = GetSuccessMessage(statusCode)
                };
            }
            else
            {
                // Handle validation errors từ FluentValidation
                var errors = ParseValidationErrors(responseText, statusCode);
                
                wrappedResponse = new ApiResponse<object>
                {
                    Success = false,
                    Data = null,
                    Message = GetErrorMessage(statusCode),
                    Errors = errors
                };
            }

            var wrappedJson = JsonSerializer.Serialize(wrappedResponse, GetJsonOptions());
            context.Response.ContentType = "application/json";
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(wrappedJson);
            
            await context.Response.WriteAsync(wrappedJson);
        }

        private List<string> ParseValidationErrors(string responseText, int statusCode)
        {
            try
            {
                if (string.IsNullOrEmpty(responseText)) return new List<string>();

                var json = JsonDocument.Parse(responseText);
                var errors = new List<string>();

                // Parse FluentValidation errors từ ModelState
                if (json.RootElement.TryGetProperty("errors", out var errorsElement))
                {
                    foreach (var error in errorsElement.EnumerateObject())
                    {
                        var fieldName = error.Name;
                        foreach (var message in error.Value.EnumerateArray())
                        {
                            var errorMessage = message.GetString();
                            if (!string.IsNullOrEmpty(errorMessage))
                            {
                                errors.Add(errorMessage);
                            }
                        }
                    }
                }
                else if (json.RootElement.TryGetProperty("title", out var titleElement))
                {
                    var title = titleElement.GetString();
                    if (!string.IsNullOrEmpty(title))
                        errors.Add(title);
                }
                else if (json.RootElement.TryGetProperty("detail", out var detailElement))
                {
                    var detail = detailElement.GetString();
                    if (!string.IsNullOrEmpty(detail))
                        errors.Add(detail);
                }

                return errors.Any() ? errors : new List<string> { GetErrorMessage(statusCode) };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to parse validation errors from response: {ResponseText}", responseText);
                return new List<string> { responseText.Length > 200 ? responseText.Substring(0, 200) + "..." : responseText };
            }
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

        private JsonSerializerOptions GetJsonOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        private string GetSuccessMessage(int statusCode) => statusCode switch
        {
            200 => "Success",
            201 => "Created",
            204 => "Success",
            _ => "Success"
        };

        private string GetErrorMessage(int statusCode) => statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            409 => "Conflict",
            422 => "Validation Error",
            500 => "Internal Server Error",
            _ => "Error"
        };

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, 
            Stream originalBodyStream)
        {
            context.Response.Body = originalBodyStream;
            context.Response.ContentType = "application/json";

            var (statusCode, message, errors) = ex switch
            {
                NotFoundException => (404, "Not Found", new List<string> { ex.Message }),
                ValidationException => (400, "Validation Error", new List<string> { ex.Message }),
                UnauthorizedAccessException => (401, "Unauthorized", new List<string> { "Please login" }),
                ArgumentException => (400, "Invalid Argument", new List<string> { ex.Message }),
                InvalidOperationException => (400, "Invalid Operation", new List<string> { ex.Message }),
                _ => (500, "Internal Server Error", new List<string> { "An unexpected error occurred" })
            };

            context.Response.StatusCode = statusCode;

            var response = new ApiResponse<object>
            {
                Success = false,
                Data = null,
                Message = message,
                Errors = errors
            };

            var jsonResponse = JsonSerializer.Serialize(response, GetJsonOptions());
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

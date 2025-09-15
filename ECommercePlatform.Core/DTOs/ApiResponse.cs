namespace ECommercePlatform.Core.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> SuccessResult(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Errors = null
            };
        }

        public static ApiResponse<T> ErrorResult(List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Errors = errors ?? new List<string>()
            };
        }
    }

    public class ApiResponse : ApiResponse<object>
    {
        public static new ApiResponse SuccessResult(object? data = null)
        {
            return new ApiResponse
            {
                Success = true,
                Data = data,
                Errors = null
            };
        }

        public static new ApiResponse ErrorResult(List<string>? errors = null)
        {
            return new ApiResponse
            {
                Success = false,
                Data = null,
                Errors = errors ?? new List<string>()
            };
        }
    }
}

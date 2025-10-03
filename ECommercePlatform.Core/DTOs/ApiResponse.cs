namespace ECommercePlatform.Core.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? ErrorCode { get; set; }

        public static ApiResponse<T> Ok(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = null,
                ErrorCode = null
            };
        }

        public static ApiResponse<T> Error(string errorCode, string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Message = message,
                ErrorCode = errorCode
            };
        }
    }

    public class ApiResponse : ApiResponse<object>
    {
        public static new ApiResponse Ok(object? data = null)
        {
            return new ApiResponse
            {
                Success = true,
                Data = data,
                Message = null,
                ErrorCode = null
            };
        }

        public static new ApiResponse Error(string errorCode, string message)
        {
            return new ApiResponse
            {
                Success = false,
                Data = null,
                Message = message,
                ErrorCode = errorCode
            };
        }
    }
}

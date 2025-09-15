
namespace ECommercePlatform.Core.DTOs
{
    // Pagination support
    public class PaginatedResponse<T> : ApiResponse<T>
    {
        public PaginationMeta? Pagination { get; set; }

        public static PaginatedResponse<T> SuccessResult(T data, PaginationMeta pagination)
        {
            return new PaginatedResponse<T>
            {
                Success = true,
                Data = data,
                Errors = null,
                Pagination = pagination
            };
        }
    }

    public class PaginationMeta
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
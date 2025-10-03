
namespace ECommercePlatform.Core.DTOs
{
    public class PaginatedResponse<T> : ApiResponse<IEnumerable<T>>
    {
        public PaginationMeta Pagination { get; set; } = new();

        public static PaginatedResponse<T> Ok(IEnumerable<T> data, PaginationMeta pagination)
        {
            return new PaginatedResponse<T>
            {
                Success = true,
                Data = data,
                Message = null,
                ErrorCode = null,
                Pagination = pagination
            };
        }

        public static PaginatedResponse<T> Ok(IEnumerable<T> data, int currentPage, int pageSize, long totalRecords)
        {
            var pagination = PaginationMeta.Create(currentPage, pageSize, totalRecords);
            return Ok(data, pagination);
        }
    }

    public class PaginationMeta
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public long TotalRecords { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public int StartRecord { get; set; }
        public int EndRecord { get; set; }

        public static PaginationMeta Create(int currentPage, int pageSize, long totalRecords)
        {
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var startRecord = (currentPage - 1) * pageSize + 1;
            var endRecord = Math.Min(currentPage * pageSize, (int)totalRecords);

            return new PaginationMeta
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalRecords = totalRecords,
                HasNext = currentPage < totalPages,
                HasPrevious = currentPage > 1,
                StartRecord = (int)startRecord,
                EndRecord = endRecord
            };
        }
    }
}
using ECommercePlatform.Core.DTOs;

namespace ECommercePlatform.Core.Extensions
{
    public static class PaginationExtensions
    {
        /// <summary>
        /// Extension method cho List/IEnumerable (in-memory pagination)
        /// </summary>
        public static PaginatedResponse<T> ToPaginatedResponse<T>(
            this IEnumerable<T> source,
            int page = 1,
            int pageSize = 10)
        {
            // Validate parameters
            var (validPage, validPageSize) = ValidatePagination(page, pageSize);

            var totalRecords = source.Count();
            var data = source
                .Skip((validPage - 1) * validPageSize)
                .Take(validPageSize)
                .ToList();

            return PaginatedResponse<T>.Ok(data, validPage, validPageSize, totalRecords);
        }

        /// <summary>
        /// Helper method để tạo pagination từ data và total count
        /// Dùng cho trường hợp đã có data và count từ database
        /// </summary>
        public static PaginatedResponse<T> CreatePaginatedResponse<T>(
            IEnumerable<T> data,
            int page,
            int pageSize,
            long totalRecords)
        {
            var (validPage, validPageSize) = ValidatePagination(page, pageSize);
            return PaginatedResponse<T>.Ok(data, validPage, validPageSize, totalRecords);
        }

        /// <summary>
        /// Extension method để validate pagination parameters
        /// </summary>
        public static (int page, int pageSize) ValidatePagination(int page, int pageSize)
        {
            return (
                Math.Max(1, page),
                Math.Max(1, Math.Min(100, pageSize)) // Max 100 items per page
            );
        }

        /// <summary>
        /// Calculate skip count for pagination
        /// </summary>
        public static int CalculateSkip(int page, int pageSize)
        {
            var (validPage, validPageSize) = ValidatePagination(page, pageSize);
            return (validPage - 1) * validPageSize;
        }
    }
}
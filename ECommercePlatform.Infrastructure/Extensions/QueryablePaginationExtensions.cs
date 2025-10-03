using ECommercePlatform.Core.DTOs;
using ECommercePlatform.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Infrastructure.Extensions
{
    /// <summary>
    /// EF Core specific pagination extensions
    /// </summary>
    public static class QueryablePaginationExtensions
    {
        /// <summary>
        /// Extension method để paginate IQueryable với EF Core
        /// </summary>
        public static async Task<PaginatedResponse<T>> ToPaginatedResponseAsync<T>(
            this IQueryable<T> query,
            int page = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            // Validate parameters
            var (validPage, validPageSize) = PaginationExtensions.ValidatePagination(page, pageSize);

            // Get total count first
            var totalRecords = await query.CountAsync(cancellationToken);

            // Get paginated data
            var data = await query
                .Skip(PaginationExtensions.CalculateSkip(validPage, validPageSize))
                .Take(validPageSize)
                .ToListAsync(cancellationToken);

            return PaginatedResponse<T>.Ok(data, validPage, validPageSize, totalRecords);
        }

        /// <summary>
        /// Get total count và paginated data trong một lần query (tối ưu hơn)
        /// </summary>
        public static async Task<PaginatedResponse<T>> ToPaginatedResponseOptimizedAsync<T>(
            this IQueryable<T> query,
            int page = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var (validPage, validPageSize) = PaginationExtensions.ValidatePagination(page, pageSize);
            var skip = PaginationExtensions.CalculateSkip(validPage, validPageSize);

            // Execute count và data queries song song
            var countTask = query.CountAsync(cancellationToken);
            var dataTask = query
                .Skip(skip)
                .Take(validPageSize)
                .ToListAsync(cancellationToken);

            await Task.WhenAll(countTask, dataTask);

            return PaginatedResponse<T>.Ok(dataTask.Result, validPage, validPageSize, countTask.Result);
        }
    }
}
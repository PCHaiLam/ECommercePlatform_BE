using System.ComponentModel.DataAnnotations;

namespace ECommercePlatform.Core.DTOs
{
    /// <summary>
    /// Base pagination request DTO
    /// </summary>
    public class PaginationRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
        public int Page { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Optional search query
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// Optional sort field
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort direction: "asc" or "desc"
        /// </summary>
        public string SortDirection { get; set; } = "asc";

        public bool IsDescending => SortDirection?.ToLower() == "desc";
    }
}
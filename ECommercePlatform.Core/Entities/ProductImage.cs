

namespace ECommercePlatform.Core.Entities
{
    public class ProductImage : BaseEntity
    {
        public long ProductId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string? AltText { get; set; }

        public int SortOrder { get; set; } = 0;

        public bool IsPrimary { get; set; } = false;

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
    }
}



namespace ECommercePlatform.Core.Entities
{
    public class WishlistItem : BaseEntity
    {
        public long WishlistId { get; set; }

        public long ProductId { get; set; }

        public long? VariantId { get; set; }

        // Navigation properties
        public virtual Wishlist Wishlist { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ProductVariant? Variant { get; set; }
    }
}

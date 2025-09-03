

namespace ECommercePlatform.Core.Entities
{
    public class Wishlist : BaseEntity
    {
        public long UserId { get; set; }

        public string Name { get; set; } = "My Wishlist";

        public bool IsDefault { get; set; } = true;

        public bool IsPublic { get; set; } = false;

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}



namespace ECommercePlatform.Core.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public long? UserId { get; set; }

        public string? SessionId { get; set; }

        public string Status { get; set; } = "active";

        public DateTime? ExpiresAt { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}



namespace ECommercePlatform.Core.Entities
{
    public class ProductReview : BaseEntity
    {
        public long ProductId { get; set; }

        public long UserId { get; set; }

        public long? OrderId { get; set; }

        public int Rating { get; set; }

        public string? Title { get; set; }

        public string? Comment { get; set; }

        public bool IsVerifiedPurchase { get; set; } = false;

        public bool IsApproved { get; set; } = false;

        public int HelpfulCount { get; set; } = 0;

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual Order? Order { get; set; }
    }
}

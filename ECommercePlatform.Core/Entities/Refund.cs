

namespace ECommercePlatform.Core.Entities
{
    public class Refund : BaseEntity
    {
        public long PaymentId { get; set; }

        public long OrderId { get; set; }

        public decimal Amount { get; set; }

        public string? Reason { get; set; }

        public string Status { get; set; } = "pending";

        public string? GatewayRefundId { get; set; }

        public DateTime? ProcessedAt { get; set; }

        // Navigation properties
        public virtual Payment Payment { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}



namespace ECommercePlatform.Core.Entities
{
    public class Payment : BaseEntity
    {
        public long OrderId { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string PaymentGateway { get; set; } = string.Empty;

        public string? TransactionId { get; set; }

        public string? ReferenceNumber { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "USD";

        public string Status { get; set; } = "pending";

        public string? GatewayResponse { get; set; }

        public DateTime? PaidAt { get; set; }

        public DateTime? FailedAt { get; set; }

        public string? FailureReason { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
    }
}

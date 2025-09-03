

namespace ECommercePlatform.Core.Entities
{
    public class Order : BaseEntity
    {
        public long? UserId { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public string Status { get; set; } = "pending";

        public string Currency { get; set; } = "USD";

        public decimal Subtotal { get; set; }

        public decimal TaxAmount { get; set; } = 0;

        public decimal ShippingAmount { get; set; } = 0;

        public decimal DiscountAmount { get; set; } = 0;

        public decimal TotalAmount { get; set; }

        // Billing Information
        public string BillingFirstName { get; set; } = string.Empty;

        public string BillingLastName { get; set; } = string.Empty;

        public string? BillingCompany { get; set; }

        public string BillingEmail { get; set; } = string.Empty;

        public string? BillingPhone { get; set; }

        public string BillingAddressLine1 { get; set; } = string.Empty;

        public string? BillingAddressLine2 { get; set; }

        public string BillingCity { get; set; } = string.Empty;

        public string BillingStateProvince { get; set; } = string.Empty;

        public string BillingPostalCode { get; set; } = string.Empty;

        public string BillingCountry { get; set; } = string.Empty;

        // Shipping Information
        public string? ShippingFirstName { get; set; }

        public string? ShippingLastName { get; set; }

        public string? ShippingCompany { get; set; }

        public string? ShippingAddressLine1 { get; set; }

        public string? ShippingAddressLine2 { get; set; }

        public string? ShippingCity { get; set; }

        public string? ShippingStateProvince { get; set; }

        public string? ShippingPostalCode { get; set; }

        public string? ShippingCountry { get; set; }

        public string? ShippingMethod { get; set; }

        public string? Notes { get; set; }

        public DateTime? ShippedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        public DateTime? CancelledAt { get; set; }

        public string? CancellationReason { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
        public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
    }
}

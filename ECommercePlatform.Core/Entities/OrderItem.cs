

namespace ECommercePlatform.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public long? VariantId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductSku { get; set; } = string.Empty;

        public string? VariantAttributes { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ProductVariant? Variant { get; set; }
    }
}

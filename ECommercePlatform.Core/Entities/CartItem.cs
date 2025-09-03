

namespace ECommercePlatform.Core.Entities
{
    public class CartItem : BaseEntity
    {
        public long CartId { get; set; }

        public long ProductId { get; set; }

        public long? VariantId { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal Price { get; set; }

        // Navigation properties
        public virtual ShoppingCart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ProductVariant? Variant { get; set; }
    }
}



namespace ECommercePlatform.Core.Entities
{
    public class ProductVariant : BaseEntity
    {
        public long ProductId { get; set; }

        public string Sku { get; set; } = string.Empty;

        public decimal? Price { get; set; }

        public decimal? SalePrice { get; set; }

        public int StockQuantity { get; set; } = 0;

        public decimal? Weight { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<ProductVariantAttribute> ProductVariantAttributes { get; set; } = new List<ProductVariantAttribute>();
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}

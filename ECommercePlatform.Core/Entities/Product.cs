

namespace ECommercePlatform.Core.Entities
{
    public class Product : BaseEntity
    {
        public long CategoryId { get; set; }

        public long? BrandId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public string Sku { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal? SalePrice { get; set; }

        public decimal? CostPrice { get; set; }

        public decimal? Weight { get; set; }

        public string? Dimensions { get; set; }

        public string Status { get; set; } = "active";

        public bool IsFeatured { get; set; } = false;

        public bool IsDigital { get; set; } = false;

        public string? MetaTitle { get; set; }

        public string? MetaDescription { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; } = null!;
        public virtual Brand? Brand { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public virtual ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
    }
}

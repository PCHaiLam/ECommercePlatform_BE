

namespace ECommercePlatform.Core.Entities
{
    public class ProductAttribute : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = "text";

        public bool IsRequired { get; set; } = false;

        public bool IsVariation { get; set; } = false;

        // Navigation properties
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
        public virtual ICollection<ProductVariantAttribute> ProductVariantAttributes { get; set; } = new List<ProductVariantAttribute>();
    }
}

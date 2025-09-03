

namespace ECommercePlatform.Core.Entities
{
    public class ProductVariantAttribute : BaseEntity
    {
        public long VariantId { get; set; }

        public long AttributeId { get; set; }

        public string Value { get; set; } = string.Empty;

        // Navigation properties
        public virtual ProductVariant Variant { get; set; } = null!;
        public virtual ProductAttribute Attribute { get; set; } = null!;
    }
}

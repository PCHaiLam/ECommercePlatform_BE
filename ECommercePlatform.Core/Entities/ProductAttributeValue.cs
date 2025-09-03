

namespace ECommercePlatform.Core.Entities
{
    public class ProductAttributeValue : BaseEntity
    {
        public long ProductId { get; set; }

        public long AttributeId { get; set; }

        public string Value { get; set; } = string.Empty;

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
        public virtual ProductAttribute Attribute { get; set; } = null!;
    }
}

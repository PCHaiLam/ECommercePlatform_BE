

namespace ECommercePlatform.Core.Entities
{
    public class Inventory : BaseEntity
    {
        public long? ProductId { get; set; }

        public long? VariantId { get; set; }

        public int StockQuantity { get; set; } = 0;

        public int ReservedQuantity { get; set; } = 0;

        public int MinStockLevel { get; set; } = 0;

        public int? MaxStockLevel { get; set; }

        public string? Location { get; set; }

        // Navigation properties
        public virtual Product? Product { get; set; }
        public virtual ProductVariant? Variant { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ECommercePlatform.Core.Entities
{
    public class ShippingMethod : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Cost { get; set; }

        public int? EstimatedDaysMin { get; set; }

        public int? EstimatedDaysMax { get; set; }

        public bool IsActive { get; set; } = true;
    }
}



namespace ECommercePlatform.Core.Entities
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public decimal Value { get; set; }

        public decimal MinimumAmount { get; set; } = 0;

        public decimal? MaximumDiscount { get; set; }

        public int? UsageLimit { get; set; }

        public int UsedCount { get; set; } = 0;

        public int UsageLimitPerUser { get; set; } = 1;

        public DateTime ValidFrom { get; set; }

        public DateTime ValidUntil { get; set; }

        public bool IsActive { get; set; } = true;

        public string? Description { get; set; }
    }
}

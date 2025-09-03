

namespace ECommercePlatform.Core.Entities
{
    public class UserAddress : BaseEntity
    {
        public long UserId { get; set; }

        public string AddressType { get; set; } = "shipping";

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Company { get; set; }

        public string AddressLine1 { get; set; } = string.Empty;

        public string? AddressLine2 { get; set; }

        public string City { get; set; } = string.Empty;

        public string StateProvince { get; set; } = string.Empty;

        public string PostalCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public bool IsDefault { get; set; } = false;

        // Navigation properties
        public virtual User User { get; set; } = null!;
    }
}

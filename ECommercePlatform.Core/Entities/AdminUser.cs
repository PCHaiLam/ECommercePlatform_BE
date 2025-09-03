

namespace ECommercePlatform.Core.Entities
{
    public class AdminUser : BaseEntity
    {
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Role { get; set; } = "admin";

        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginAt { get; set; }
    }
}

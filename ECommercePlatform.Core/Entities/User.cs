

namespace ECommercePlatform.Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? AvatarUrl { get; set; }

        public bool EmailVerified { get; set; } = false;

        public bool PhoneVerified { get; set; } = false;

        public string Status { get; set; } = "active";

        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public virtual ICollection<EmailLog> EmailLogs { get; set; } = new List<EmailLog>();
    }
}

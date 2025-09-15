namespace ECommercePlatform.Core.Entities
{
    public class RefreshToken : BaseEntity
    {
        public long UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }

        // Navigation
        public User User { get; set; } = null!;
    }
}



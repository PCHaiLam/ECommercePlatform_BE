

namespace ECommercePlatform.Core.Entities
{
    public class Notification : BaseEntity
    {
        public long UserId { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public DateTime? ReadAt { get; set; }

        public string? Data { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;
    }
}

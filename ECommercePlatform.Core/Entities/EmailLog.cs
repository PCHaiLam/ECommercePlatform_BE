

namespace ECommercePlatform.Core.Entities
{
    public class EmailLog : BaseEntity
    {
        public long? UserId { get; set; }

        public string EmailAddress { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string? TemplateName { get; set; }

        public string Status { get; set; } = "pending";

        public DateTime? SentAt { get; set; }

        public DateTime? FailedAt { get; set; }

        public string? ErrorMessage { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
    }
}

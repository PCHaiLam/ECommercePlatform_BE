using System.ComponentModel.DataAnnotations;

namespace ECommercePlatform.Core.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool Deleted { get; set; } = false;
    }
}

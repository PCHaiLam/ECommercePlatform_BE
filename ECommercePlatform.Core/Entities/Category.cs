

namespace ECommercePlatform.Core.Entities
{
    public class Category : BaseEntity
    {
        public long? ParentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int SortOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public string? MetaTitle { get; set; }

        public string? MetaDescription { get; set; }

        // Navigation properties
        public virtual Category? Parent { get; set; }
        public virtual ICollection<Category> Children { get; set; } = new List<Category>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

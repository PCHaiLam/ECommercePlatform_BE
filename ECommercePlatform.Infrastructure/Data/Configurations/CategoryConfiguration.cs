using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("category");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id");

            builder.Property(c => c.ParentId)
                .HasColumnName("parent_id");

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Slug)
                .HasColumnName("slug")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasColumnName("description")
                .HasColumnType("ntext");

            builder.Property(c => c.ImageUrl)
                .HasColumnName("image_url")
                .HasMaxLength(500);

            builder.Property(c => c.SortOrder)
                .HasColumnName("sort_order")
                .HasDefaultValue(0);

            builder.Property(c => c.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(c => c.MetaTitle)
                .HasColumnName("meta_title")
                .HasMaxLength(255);

            builder.Property(c => c.MetaDescription)
                .HasColumnName("meta_description")
                .HasMaxLength(500);

            builder.Property(c => c.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(c => c.ParentId)
                .HasDatabaseName("IX_categories_parent_id")
                .HasFilter("deleted = 0");

            builder.HasIndex(c => c.Slug)
                .HasDatabaseName("IX_categories_slug")
                .HasFilter("deleted = 0")
                .IsUnique();

            builder.HasIndex(c => c.IsActive)
                .HasDatabaseName("IX_categories_active")
                .HasFilter("deleted = 0");

            // Relationships
            builder.HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Children)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

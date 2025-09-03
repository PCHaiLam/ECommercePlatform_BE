using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("brand");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).HasColumnName("id");

            builder.Property(b => b.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(b => b.Slug)
                .HasColumnName("slug")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(b => b.Description)
                .HasColumnName("description")
                .HasColumnType("ntext");

            builder.Property(b => b.LogoUrl)
                .HasColumnName("logo_url")
                .HasMaxLength(500);

            builder.Property(b => b.WebsiteUrl)
                .HasColumnName("website_url")
                .HasMaxLength(500);

            builder.Property(b => b.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(b => b.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(b => b.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(b => b.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(b => b.Name)
                .HasDatabaseName("IX_brands_name")
                .HasFilter("deleted = 0")
                .IsUnique();

            builder.HasIndex(b => b.Slug)
                .HasDatabaseName("IX_brands_slug")
                .HasFilter("deleted = 0")
                .IsUnique();

            // Relationships
            builder.HasMany(b => b.Products)
                .WithOne(p => p.Brand)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

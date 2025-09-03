using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("product_image");

            builder.HasKey(pi => pi.Id);
            builder.Property(pi => pi.Id).HasColumnName("id");

            builder.Property(pi => pi.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(pi => pi.ImageUrl)
                .HasColumnName("image_url")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(pi => pi.AltText)
                .HasColumnName("alt_text")
                .HasMaxLength(255);

            builder.Property(pi => pi.SortOrder)
                .HasColumnName("sort_order")
                .HasDefaultValue(0);

            builder.Property(pi => pi.IsPrimary)
                .HasColumnName("is_primary")
                .HasDefaultValue(false);

            builder.Property(pi => pi.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(pi => pi.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pi => pi.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
    {
        public void Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            builder.ToTable("wishlist_item");

            builder.HasKey(wi => wi.Id);
            builder.Property(wi => wi.Id).HasColumnName("id");

            builder.Property(wi => wi.WishlistId)
                .HasColumnName("wishlist_id")
                .IsRequired();

            builder.Property(wi => wi.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(wi => wi.VariantId)
                .HasColumnName("variant_id");

            builder.Property(wi => wi.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(wi => wi.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(wi => wi.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(wi => wi.Wishlist)
                .WithMany(w => w.WishlistItems)
                .HasForeignKey(wi => wi.WishlistId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wi => wi.Product)
                .WithMany(p => p.WishlistItems)
                .HasForeignKey(wi => wi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wi => wi.Variant)
                .WithMany(pv => pv.WishlistItems)
                .HasForeignKey(wi => wi.VariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

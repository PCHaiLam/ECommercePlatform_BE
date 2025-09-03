using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("cart_item");

            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Id).HasColumnName("id");

            builder.Property(ci => ci.CartId)
                .HasColumnName("cart_id")
                .IsRequired();

            builder.Property(ci => ci.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(ci => ci.VariantId)
                .HasColumnName("variant_id");

            builder.Property(ci => ci.Quantity)
                .HasColumnName("quantity")
                .HasDefaultValue(1);

            builder.Property(ci => ci.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(ci => ci.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(ci => ci.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(ci => ci.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(ci => ci.Cart)
                .WithMany(sc => sc.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ci => ci.Variant)
                .WithMany(pv => pv.CartItems)
                .HasForeignKey(ci => ci.VariantId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

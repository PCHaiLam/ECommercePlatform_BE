using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.ToTable("product_variant");

            builder.HasKey(pv => pv.Id);
            builder.Property(pv => pv.Id).HasColumnName("id");

            builder.Property(pv => pv.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(pv => pv.Sku)
                .HasColumnName("sku")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(pv => pv.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)");

            builder.Property(pv => pv.SalePrice)
                .HasColumnName("sale_price")
                .HasColumnType("decimal(10,2)");

            builder.Property(pv => pv.StockQuantity)
                .HasColumnName("stock_quantity")
                .HasDefaultValue(0);

            builder.Property(pv => pv.Weight)
                .HasColumnName("weight")
                .HasColumnType("decimal(8,2)");

            builder.Property(pv => pv.ImageUrl)
                .HasColumnName("image_url")
                .HasMaxLength(500);

            builder.Property(pv => pv.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(pv => pv.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(pv => pv.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pv => pv.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(pv => pv.Sku)
                .HasDatabaseName("IX_product_variants_sku")
                .HasFilter("deleted = 0")
                .IsUnique();

            // Relationships
            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.ProductVariants)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pv => pv.ProductVariantAttributes)
                .WithOne(pva => pva.Variant)
                .HasForeignKey(pva => pva.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pv => pv.Inventories)
                .WithOne(i => i.Variant)
                .HasForeignKey(i => i.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pv => pv.CartItems)
                .WithOne(ci => ci.Variant)
                .HasForeignKey(ci => ci.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pv => pv.OrderItems)
                .WithOne(oi => oi.Variant)
                .HasForeignKey(oi => oi.VariantId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(pv => pv.WishlistItems)
                .WithOne(wi => wi.Variant)
                .HasForeignKey(wi => wi.VariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

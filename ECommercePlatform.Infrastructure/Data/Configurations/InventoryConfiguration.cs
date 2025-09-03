using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("inventory");

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).HasColumnName("id");

            builder.Property(i => i.ProductId)
                .HasColumnName("product_id");

            builder.Property(i => i.VariantId)
                .HasColumnName("variant_id");

            builder.Property(i => i.StockQuantity)
                .HasColumnName("stock_quantity")
                .HasDefaultValue(0);

            builder.Property(i => i.ReservedQuantity)
                .HasColumnName("reserved_quantity")
                .HasDefaultValue(0);

            builder.Property(i => i.MinStockLevel)
                .HasColumnName("min_stock_level")
                .HasDefaultValue(0);

            builder.Property(i => i.MaxStockLevel)
                .HasColumnName("max_stock_level");

            builder.Property(i => i.Location)
                .HasColumnName("location")
                .HasMaxLength(255);

            builder.Property(i => i.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(i => i.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(i => i.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(i => i.ProductId)
                .HasDatabaseName("IX_inventory_product_id")
                .HasFilter("deleted = 0");

            builder.HasIndex(i => i.VariantId)
                .HasDatabaseName("IX_inventory_variant_id")
                .HasFilter("deleted = 0");

            // Constraints
            builder.HasCheckConstraint("CK_inventory_product_or_variant", "(product_id IS NOT NULL AND variant_id IS NULL) OR (product_id IS NULL AND variant_id IS NOT NULL)");

            // Relationships
            builder.HasOne(i => i.Product)
                .WithMany(p => p.Inventories)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Variant)
                .WithMany(pv => pv.Inventories)
                .HasForeignKey(i => i.VariantId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

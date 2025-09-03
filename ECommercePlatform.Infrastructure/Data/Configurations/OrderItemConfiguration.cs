using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("order_item");

            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Id).HasColumnName("id");

            builder.Property(oi => oi.OrderId)
                .HasColumnName("order_id")
                .IsRequired();

            builder.Property(oi => oi.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(oi => oi.VariantId)
                .HasColumnName("variant_id");

            builder.Property(oi => oi.ProductName)
                .HasColumnName("product_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(oi => oi.ProductSku)
                .HasColumnName("product_sku")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(oi => oi.VariantAttributes)
                .HasColumnName("variant_attributes")
                .HasMaxLength(500);

            builder.Property(oi => oi.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                .HasColumnName("unit_price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(oi => oi.TotalPrice)
                .HasColumnName("total_price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(oi => oi.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(oi => oi.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(oi => oi.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(oi => oi.Variant)
                .WithMany(pv => pv.OrderItems)
                .HasForeignKey(oi => oi.VariantId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("order");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");

            builder.Property(o => o.UserId)
                .HasColumnName("user_id");

            builder.Property(o => o.OrderNumber)
                .HasColumnName("order_number")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.Status)
                .HasColumnName("status")
                .HasMaxLength(30)
                .HasDefaultValue("pending");

            builder.Property(o => o.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .HasDefaultValue("USD");

            builder.Property(o => o.Subtotal)
                .HasColumnName("subtotal")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(o => o.TaxAmount)
                .HasColumnName("tax_amount")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            builder.Property(o => o.ShippingAmount)
                .HasColumnName("shipping_amount")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            builder.Property(o => o.DiscountAmount)
                .HasColumnName("discount_amount")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            builder.Property(o => o.TotalAmount)
                .HasColumnName("total_amount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            // Billing Information
            builder.Property(o => o.BillingFirstName)
                .HasColumnName("billing_first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.BillingLastName)
                .HasColumnName("billing_last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.BillingCompany)
                .HasColumnName("billing_company")
                .HasMaxLength(255);

            builder.Property(o => o.BillingEmail)
                .HasColumnName("billing_email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(o => o.BillingPhone)
                .HasColumnName("billing_phone")
                .HasMaxLength(20);

            builder.Property(o => o.BillingAddressLine1)
                .HasColumnName("billing_address_line_1")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(o => o.BillingAddressLine2)
                .HasColumnName("billing_address_line_2")
                .HasMaxLength(255);

            builder.Property(o => o.BillingCity)
                .HasColumnName("billing_city")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.BillingStateProvince)
                .HasColumnName("billing_state_province")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(o => o.BillingPostalCode)
                .HasColumnName("billing_postal_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(o => o.BillingCountry)
                .HasColumnName("billing_country")
                .HasMaxLength(100)
                .IsRequired();

            // Shipping Information
            builder.Property(o => o.ShippingFirstName)
                .HasColumnName("shipping_first_name")
                .HasMaxLength(100);

            builder.Property(o => o.ShippingLastName)
                .HasColumnName("shipping_last_name")
                .HasMaxLength(100);

            builder.Property(o => o.ShippingCompany)
                .HasColumnName("shipping_company")
                .HasMaxLength(255);

            builder.Property(o => o.ShippingAddressLine1)
                .HasColumnName("shipping_address_line_1")
                .HasMaxLength(255);

            builder.Property(o => o.ShippingAddressLine2)
                .HasColumnName("shipping_address_line_2")
                .HasMaxLength(255);

            builder.Property(o => o.ShippingCity)
                .HasColumnName("shipping_city")
                .HasMaxLength(100);

            builder.Property(o => o.ShippingStateProvince)
                .HasColumnName("shipping_state_province")
                .HasMaxLength(100);

            builder.Property(o => o.ShippingPostalCode)
                .HasColumnName("shipping_postal_code")
                .HasMaxLength(20);

            builder.Property(o => o.ShippingCountry)
                .HasColumnName("shipping_country")
                .HasMaxLength(100);

            builder.Property(o => o.ShippingMethod)
                .HasColumnName("shipping_method")
                .HasMaxLength(100);

            builder.Property(o => o.Notes)
                .HasColumnName("notes")
                .HasColumnType("ntext");

            builder.Property(o => o.ShippedAt)
                .HasColumnName("shipped_at")
                .HasColumnType("datetime2");

            builder.Property(o => o.DeliveredAt)
                .HasColumnName("delivered_at")
                .HasColumnType("datetime2");

            builder.Property(o => o.CancelledAt)
                .HasColumnName("cancelled_at")
                .HasColumnType("datetime2");

            builder.Property(o => o.CancellationReason)
                .HasColumnName("cancellation_reason")
                .HasMaxLength(500);

            builder.Property(o => o.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(o => o.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(o => o.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(o => o.UserId)
                .HasDatabaseName("IX_orders_user_id")
                .HasFilter("deleted = 0");

            builder.HasIndex(o => o.Status)
                .HasDatabaseName("IX_orders_status")
                .HasFilter("deleted = 0");

            builder.HasIndex(o => o.CreatedAt)
                .HasDatabaseName("IX_orders_created_at")
                .HasFilter("deleted = 0");

            builder.HasIndex(o => o.OrderNumber)
                .HasDatabaseName("IX_orders_order_number")
                .HasFilter("deleted = 0")
                .IsUnique();

            // Constraints
            builder.HasCheckConstraint("CK_orders_status", "status IN ('pending', 'processing', 'shipped', 'delivered', 'cancelled', 'refunded')");

            // Relationships
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Refunds)
                .WithOne(r => r.Order)
                .HasForeignKey(r => r.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.ProductReviews)
                .WithOne(pr => pr.Order)
                .HasForeignKey(pr => pr.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

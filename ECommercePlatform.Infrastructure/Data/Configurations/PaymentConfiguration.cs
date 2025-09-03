using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payment");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.OrderId)
                .HasColumnName("order_id")
                .IsRequired();

            builder.Property(p => p.PaymentMethod)
                .HasColumnName("payment_method")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.PaymentGateway)
                .HasColumnName("payment_gateway")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.TransactionId)
                .HasColumnName("transaction_id")
                .HasMaxLength(255);

            builder.Property(p => p.ReferenceNumber)
                .HasColumnName("reference_number")
                .HasMaxLength(255);

            builder.Property(p => p.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .HasDefaultValue("USD");

            builder.Property(p => p.Status)
                .HasColumnName("status")
                .HasMaxLength(30)
                .HasDefaultValue("pending");

            builder.Property(p => p.GatewayResponse)
                .HasColumnName("gateway_response")
                .HasColumnType("ntext");

            builder.Property(p => p.PaidAt)
                .HasColumnName("paid_at")
                .HasColumnType("datetime2");

            builder.Property(p => p.FailedAt)
                .HasColumnName("failed_at")
                .HasColumnType("datetime2");

            builder.Property(p => p.FailureReason)
                .HasColumnName("failure_reason")
                .HasMaxLength(500);

            builder.Property(p => p.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Constraints
            builder.HasCheckConstraint("CK_payments_status", "status IN ('pending', 'processing', 'completed', 'failed', 'cancelled', 'refunded')");

            // Relationships
            builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Refunds)
                .WithOne(r => r.Payment)
                .HasForeignKey(r => r.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

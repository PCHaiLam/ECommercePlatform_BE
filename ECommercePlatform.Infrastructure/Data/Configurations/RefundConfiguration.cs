using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class RefundConfiguration : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable("refund");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnName("id");

            builder.Property(r => r.PaymentId)
                .HasColumnName("payment_id")
                .IsRequired();

            builder.Property(r => r.OrderId)
                .HasColumnName("order_id")
                .IsRequired();

            builder.Property(r => r.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(r => r.Reason)
                .HasColumnName("reason")
                .HasMaxLength(500);

            builder.Property(r => r.Status)
                .HasColumnName("status")
                .HasMaxLength(30)
                .HasDefaultValue("pending");

            builder.Property(r => r.GatewayRefundId)
                .HasColumnName("gateway_refund_id")
                .HasMaxLength(255);

            builder.Property(r => r.ProcessedAt)
                .HasColumnName("processed_at")
                .HasColumnType("datetime2");

            builder.Property(r => r.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(r => r.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(r => r.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Constraints
            builder.HasCheckConstraint("CK_refunds_status", "status IN ('pending', 'processing', 'completed', 'failed')");

            // Relationships
            builder.HasOne(r => r.Payment)
                .WithMany(p => p.Refunds)
                .HasForeignKey(r => r.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Order)
                .WithMany(o => o.Refunds)
                .HasForeignKey(r => r.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

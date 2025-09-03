using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("coupon");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id");

            builder.Property(c => c.Code)
                .HasColumnName("code")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Type)
                .HasColumnName("type")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(c => c.Value)
                .HasColumnName("value")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(c => c.MinimumAmount)
                .HasColumnName("minimum_amount")
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            builder.Property(c => c.MaximumDiscount)
                .HasColumnName("maximum_discount")
                .HasColumnType("decimal(10,2)");

            builder.Property(c => c.UsageLimit)
                .HasColumnName("usage_limit");

            builder.Property(c => c.UsedCount)
                .HasColumnName("used_count")
                .HasDefaultValue(0);

            builder.Property(c => c.UsageLimitPerUser)
                .HasColumnName("usage_limit_per_user")
                .HasDefaultValue(1);

            builder.Property(c => c.ValidFrom)
                .HasColumnName("valid_from")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.ValidUntil)
                .HasColumnName("valid_until")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(c => c.Description)
                .HasColumnName("description")
                .HasMaxLength(500);

            builder.Property(c => c.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(c => c.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(c => c.Code)
                .HasDatabaseName("IX_coupons_code")
                .HasFilter("deleted = 0")
                .IsUnique();

            // Constraints
            builder.HasCheckConstraint("CK_coupons_type", "type IN ('percentage', 'fixed_amount', 'free_shipping')");
        }
    }
}

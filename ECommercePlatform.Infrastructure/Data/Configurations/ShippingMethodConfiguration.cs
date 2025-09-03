using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class ShippingMethodConfiguration : IEntityTypeConfiguration<ShippingMethod>
    {
        public void Configure(EntityTypeBuilder<ShippingMethod> builder)
        {
            builder.ToTable("shipping_method");

            builder.HasKey(sm => sm.Id);
            builder.Property(sm => sm.Id).HasColumnName("id");

            builder.Property(sm => sm.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(sm => sm.Description)
                .HasColumnName("description")
                .HasMaxLength(500);

            builder.Property(sm => sm.Cost)
                .HasColumnName("cost")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(sm => sm.EstimatedDaysMin)
                .HasColumnName("estimated_days_min");

            builder.Property(sm => sm.EstimatedDaysMax)
                .HasColumnName("estimated_days_max");

            builder.Property(sm => sm.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(sm => sm.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(sm => sm.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(sm => sm.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

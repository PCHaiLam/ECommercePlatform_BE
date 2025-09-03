using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("shopping_cart");

            builder.HasKey(sc => sc.Id);
            builder.Property(sc => sc.Id).HasColumnName("id");

            builder.Property(sc => sc.UserId)
                .HasColumnName("user_id");

            builder.Property(sc => sc.SessionId)
                .HasColumnName("session_id")
                .HasMaxLength(255);

            builder.Property(sc => sc.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasDefaultValue("active");

            builder.Property(sc => sc.ExpiresAt)
                .HasColumnName("expires_at")
                .HasColumnType("datetime2");

            builder.Property(sc => sc.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(sc => sc.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(sc => sc.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Constraints
            builder.HasCheckConstraint("CK_shopping_carts_status", "status IN ('active', 'abandoned', 'converted')");

            // Relationships
            builder.HasOne(sc => sc.User)
                .WithMany(u => u.ShoppingCarts)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(sc => sc.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

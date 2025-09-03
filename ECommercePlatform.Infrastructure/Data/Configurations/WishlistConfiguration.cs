using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("wishlist");

            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).HasColumnName("id");

            builder.Property(w => w.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(w => w.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .HasDefaultValue("My Wishlist");

            builder.Property(w => w.IsDefault)
                .HasColumnName("is_default")
                .HasDefaultValue(true);

            builder.Property(w => w.IsPublic)
                .HasColumnName("is_public")
                .HasDefaultValue(false);

            builder.Property(w => w.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(w => w.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(w => w.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(w => w.User)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(w => w.WishlistItems)
                .WithOne(wi => wi.Wishlist)
                .HasForeignKey(wi => wi.WishlistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

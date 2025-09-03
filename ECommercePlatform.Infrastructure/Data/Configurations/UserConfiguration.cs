using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", t => 
            {
                t.HasCheckConstraint("CK_users_gender", "gender IN ('male', 'female', 'other')");
                t.HasCheckConstraint("CK_users_status", "status IN ('active', 'inactive', 'banned')");
            });

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20);

            builder.Property(u => u.DateOfBirth)
                .HasColumnName("date_of_birth")
                .HasColumnType("date");

            builder.Property(u => u.Gender)
                .HasColumnName("gender")
                .HasMaxLength(10);

            builder.Property(u => u.AvatarUrl)
                .HasColumnName("avatar_url")
                .HasMaxLength(500);

            builder.Property(u => u.EmailVerified)
                .HasColumnName("email_verified")
                .HasDefaultValue(false);

            builder.Property(u => u.PhoneVerified)
                .HasColumnName("phone_verified")
                .HasDefaultValue(false);

            builder.Property(u => u.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasDefaultValue("active");

            builder.Property(u => u.LastLoginAt)
                .HasColumnName("last_login_at")
                .HasColumnType("datetime2");

            builder.Property(u => u.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(u => u.Email)
                .HasDatabaseName("IX_users_email")
                .HasFilter("deleted = 0");

            builder.HasIndex(u => u.Status)
                .HasDatabaseName("IX_users_status")
                .HasFilter("deleted = 0");



            // Relationships
            builder.HasMany(u => u.UserAddresses)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.ShoppingCarts)
                .WithOne(sc => sc.User)
                .HasForeignKey(sc => sc.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.ProductReviews)
                .WithOne(pr => pr.User)
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Wishlists)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.EmailLogs)
                .WithOne(el => el.User)
                .HasForeignKey(el => el.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

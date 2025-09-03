using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.ToTable("admin_user");

            builder.HasKey(au => au.Id);
            builder.Property(au => au.Id).HasColumnName("id");

            builder.Property(au => au.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(au => au.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(au => au.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(au => au.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(au => au.Role)
                .HasColumnName("role")
                .HasMaxLength(50)
                .HasDefaultValue("admin");

            builder.Property(au => au.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            builder.Property(au => au.LastLoginAt)
                .HasColumnName("last_login_at")
                .HasColumnType("datetime2");

            builder.Property(au => au.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(au => au.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(au => au.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(au => au.Email)
                .HasDatabaseName("IX_admin_users_email")
                .HasFilter("deleted = 0")
                .IsUnique();

            // Constraints
            builder.HasCheckConstraint("CK_admin_users_role", "role IN ('super_admin', 'admin', 'moderator')");
        }
    }
}

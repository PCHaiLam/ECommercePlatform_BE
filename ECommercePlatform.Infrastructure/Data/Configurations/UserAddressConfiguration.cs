using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("user_address");

            builder.HasKey(ua => ua.Id);
            builder.Property(ua => ua.Id).HasColumnName("id");

            builder.Property(ua => ua.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(ua => ua.AddressType)
                .HasColumnName("address_type")
                .HasMaxLength(20)
                .HasDefaultValue("shipping");

            builder.Property(ua => ua.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ua => ua.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ua => ua.Company)
                .HasColumnName("company")
                .HasMaxLength(255);

            builder.Property(ua => ua.AddressLine1)
                .HasColumnName("address_line_1")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(ua => ua.AddressLine2)
                .HasColumnName("address_line_2")
                .HasMaxLength(255);

            builder.Property(ua => ua.City)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ua => ua.StateProvince)
                .HasColumnName("state_province")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ua => ua.PostalCode)
                .HasColumnName("postal_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(ua => ua.Country)
                .HasColumnName("country")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(ua => ua.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20);

            builder.Property(ua => ua.IsDefault)
                .HasColumnName("is_default")
                .HasDefaultValue(false);

            builder.Property(ua => ua.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(ua => ua.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(ua => ua.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Constraints
            builder.HasCheckConstraint("CK_user_addresses_address_type", "address_type IN ('shipping', 'billing', 'both')");

            // Relationships
            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserAddresses)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

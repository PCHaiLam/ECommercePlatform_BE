using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class SystemSettingConfiguration : IEntityTypeConfiguration<SystemSetting>
    {
        public void Configure(EntityTypeBuilder<SystemSetting> builder)
        {
            builder.ToTable("system_setting");

            builder.HasKey(ss => ss.Id);
            builder.Property(ss => ss.Id).HasColumnName("id");

            builder.Property(ss => ss.KeyName)
                .HasColumnName("key_name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(ss => ss.Value)
                .HasColumnName("value")
                .HasColumnType("ntext");

            builder.Property(ss => ss.DataType)
                .HasColumnName("data_type")
                .HasMaxLength(20)
                .HasDefaultValue("string");

            builder.Property(ss => ss.Description)
                .HasColumnName("description")
                .HasMaxLength(500);

            builder.Property(ss => ss.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(ss => ss.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(ss => ss.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(ss => ss.KeyName)
                .HasDatabaseName("IX_system_settings_key_name")
                .HasFilter("deleted = 0")
                .IsUnique();

            // Constraints
            builder.HasCheckConstraint("CK_system_settings_data_type", "data_type IN ('string', 'integer', 'decimal', 'boolean', 'json')");
        }
    }
}

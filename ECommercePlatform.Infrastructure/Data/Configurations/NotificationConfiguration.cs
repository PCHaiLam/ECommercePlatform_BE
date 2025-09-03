using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("notification");

            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).HasColumnName("id");

            builder.Property(n => n.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(n => n.Type)
                .HasColumnName("type")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(n => n.Title)
                .HasColumnName("title")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(n => n.Message)
                .HasColumnName("message")
                .HasColumnType("ntext")
                .IsRequired();

            builder.Property(n => n.IsRead)
                .HasColumnName("is_read")
                .HasDefaultValue(false);

            builder.Property(n => n.ReadAt)
                .HasColumnName("read_at")
                .HasColumnType("datetime2");

            builder.Property(n => n.Data)
                .HasColumnName("data")
                .HasColumnType("ntext");

            builder.Property(n => n.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(n => n.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(n => n.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

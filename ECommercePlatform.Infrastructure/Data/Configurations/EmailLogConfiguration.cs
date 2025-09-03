using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class EmailLogConfiguration : IEntityTypeConfiguration<EmailLog>
    {
        public void Configure(EntityTypeBuilder<EmailLog> builder)
        {
            builder.ToTable("email_log");

            builder.HasKey(el => el.Id);
            builder.Property(el => el.Id).HasColumnName("id");

            builder.Property(el => el.UserId)
                .HasColumnName("user_id");

            builder.Property(el => el.EmailAddress)
                .HasColumnName("email_address")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(el => el.Subject)
                .HasColumnName("subject")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(el => el.TemplateName)
                .HasColumnName("template_name")
                .HasMaxLength(100);

            builder.Property(el => el.Status)
                .HasColumnName("status")
                .HasMaxLength(30)
                .HasDefaultValue("pending");

            builder.Property(el => el.SentAt)
                .HasColumnName("sent_at")
                .HasColumnType("datetime2");

            builder.Property(el => el.FailedAt)
                .HasColumnName("failed_at")
                .HasColumnType("datetime2");

            builder.Property(el => el.ErrorMessage)
                .HasColumnName("error_message")
                .HasMaxLength(1000);

            builder.Property(el => el.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(el => el.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(el => el.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Constraints
            builder.HasCheckConstraint("CK_email_logs_status", "status IN ('pending', 'sent', 'failed', 'bounced')");

            // Relationships
            builder.HasOne(el => el.User)
                .WithMany(u => u.EmailLogs)
                .HasForeignKey(el => el.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

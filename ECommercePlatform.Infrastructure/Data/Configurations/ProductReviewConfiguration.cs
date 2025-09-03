using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.ToTable("product_review");

            builder.HasKey(pr => pr.Id);
            builder.Property(pr => pr.Id).HasColumnName("id");

            builder.Property(pr => pr.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(pr => pr.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            builder.Property(pr => pr.OrderId)
                .HasColumnName("order_id");

            builder.Property(pr => pr.Rating)
                .HasColumnName("rating")
                .IsRequired();

            builder.Property(pr => pr.Title)
                .HasColumnName("title")
                .HasMaxLength(255);

            builder.Property(pr => pr.Comment)
                .HasColumnName("comment")
                .HasColumnType("ntext");

            builder.Property(pr => pr.IsVerifiedPurchase)
                .HasColumnName("is_verified_purchase")
                .HasDefaultValue(false);

            builder.Property(pr => pr.IsApproved)
                .HasColumnName("is_approved")
                .HasDefaultValue(false);

            builder.Property(pr => pr.HelpfulCount)
                .HasColumnName("helpful_count")
                .HasDefaultValue(0);

            builder.Property(pr => pr.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(pr => pr.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pr => pr.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(pr => pr.ProductId)
                .HasDatabaseName("IX_product_reviews_product_id")
                .HasFilter("deleted = 0");

            builder.HasIndex(pr => pr.UserId)
                .HasDatabaseName("IX_product_reviews_user_id")
                .HasFilter("deleted = 0");

            builder.HasIndex(pr => pr.Rating)
                .HasDatabaseName("IX_product_reviews_rating")
                .HasFilter("deleted = 0");

            // Constraints
            builder.HasCheckConstraint("CK_product_reviews_rating", "rating BETWEEN 1 AND 5");

            // Relationships
            builder.HasOne(pr => pr.Product)
                .WithMany(p => p.ProductReviews)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pr => pr.User)
                .WithMany(u => u.ProductReviews)
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pr => pr.Order)
                .WithMany(o => o.ProductReviews)
                .HasForeignKey(pr => pr.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

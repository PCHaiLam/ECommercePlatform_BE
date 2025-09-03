using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
        {
            builder.ToTable("product_attribute_value");

            builder.HasKey(pav => pav.Id);
            builder.Property(pav => pav.Id).HasColumnName("id");

            builder.Property(pav => pav.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property(pav => pav.AttributeId)
                .HasColumnName("attribute_id")
                .IsRequired();

            builder.Property(pav => pav.Value)
                .HasColumnName("value")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(pav => pav.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(pav => pav.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pav => pav.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(pav => pav.Product)
                .WithMany(p => p.ProductAttributeValues)
                .HasForeignKey(pav => pav.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pav => pav.Attribute)
                .WithMany(pa => pa.ProductAttributeValues)
                .HasForeignKey(pav => pav.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("product_attribute");

            builder.HasKey(pa => pa.Id);
            builder.Property(pa => pa.Id).HasColumnName("id");

            builder.Property(pa => pa.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(pa => pa.Type)
                .HasColumnName("type")
                .HasMaxLength(50)
                .HasDefaultValue("text");

            builder.Property(pa => pa.IsRequired)
                .HasColumnName("is_required")
                .HasDefaultValue(false);

            builder.Property(pa => pa.IsVariation)
                .HasColumnName("is_variation")
                .HasDefaultValue(false);

            builder.Property(pa => pa.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(pa => pa.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pa => pa.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Constraints
            builder.HasCheckConstraint("CK_product_attributes_type", "type IN ('text', 'number', 'boolean', 'date', 'select')");

            // Relationships
            builder.HasMany(pa => pa.ProductAttributeValues)
                .WithOne(pav => pav.Attribute)
                .HasForeignKey(pav => pav.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pa => pa.ProductVariantAttributes)
                .WithOne(pva => pva.Attribute)
                .HasForeignKey(pva => pva.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

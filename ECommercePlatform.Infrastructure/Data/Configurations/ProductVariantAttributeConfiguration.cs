using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class ProductVariantAttributeConfiguration : IEntityTypeConfiguration<ProductVariantAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductVariantAttribute> builder)
        {
            builder.ToTable("product_variant_attribute");

            builder.HasKey(pva => pva.Id);
            builder.Property(pva => pva.Id).HasColumnName("id");

            builder.Property(pva => pva.VariantId)
                .HasColumnName("variant_id")
                .IsRequired();

            builder.Property(pva => pva.AttributeId)
                .HasColumnName("attribute_id")
                .IsRequired();

            builder.Property(pva => pva.Value)
                .HasColumnName("value")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(pva => pva.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(pva => pva.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pva => pva.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(pva => pva.Variant)
                .WithMany(pv => pv.ProductVariantAttributes)
                .HasForeignKey(pva => pva.VariantId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pva => pva.Attribute)
                .WithMany(pa => pa.ProductVariantAttributes)
                .HasForeignKey(pva => pva.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

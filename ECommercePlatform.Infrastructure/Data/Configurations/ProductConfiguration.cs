using ECommercePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePlatform.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product", t => 
            {
                t.HasCheckConstraint("CK_products_status", "status IN ('active', 'inactive', 'draft', 'out_of_stock')");
            });

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("id");

            builder.Property(p => p.CategoryId)
                .HasColumnName("category_id")
                .IsRequired();

            builder.Property(p => p.BrandId)
                .HasColumnName("brand_id");

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Slug)
                .HasColumnName("slug")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.ShortDescription)
                .HasColumnName("short_description")
                .HasMaxLength(500);

            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("ntext");

            builder.Property(p => p.Sku)
                .HasColumnName("sku")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(p => p.SalePrice)
                .HasColumnName("sale_price")
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.CostPrice)
                .HasColumnName("cost_price")
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Weight)
                .HasColumnName("weight")
                .HasColumnType("decimal(8,2)");

            builder.Property(p => p.Dimensions)
                .HasColumnName("dimensions")
                .HasMaxLength(100);

            builder.Property(p => p.Status)
                .HasColumnName("status")
                .HasMaxLength(20)
                .HasDefaultValue("active");

            builder.Property(p => p.IsFeatured)
                .HasColumnName("is_featured")
                .HasDefaultValue(false);

            builder.Property(p => p.IsDigital)
                .HasColumnName("is_digital")
                .HasDefaultValue(false);

            builder.Property(p => p.MetaTitle)
                .HasColumnName("meta_title")
                .HasMaxLength(255);

            builder.Property(p => p.MetaDescription)
                .HasColumnName("meta_description")
                .HasMaxLength(500);

            builder.Property(p => p.Deleted)
                .HasColumnName("deleted")
                .HasDefaultValue(false);

            builder.Property(p => p.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.UpdatedAt)
                .HasColumnName("updated_at")
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(p => p.CategoryId)
                .HasDatabaseName("IX_products_category_id")
                .HasFilter("deleted = 0");

            builder.HasIndex(p => p.BrandId)
                .HasDatabaseName("IX_products_brand_id")
                .HasFilter("deleted = 0");

            builder.HasIndex(p => p.Status)
                .HasDatabaseName("IX_products_status")
                .HasFilter("deleted = 0");

            builder.HasIndex(p => p.Sku)
                .HasDatabaseName("IX_products_sku")
                .HasFilter("deleted = 0")
                .IsUnique();

            builder.HasIndex(p => p.Slug)
                .HasDatabaseName("IX_products_slug")
                .HasFilter("deleted = 0")
                .IsUnique();

            builder.HasIndex(p => p.Price)
                .HasDatabaseName("IX_products_price")
                .HasFilter("deleted = 0");

            builder.HasIndex(p => p.IsFeatured)
                .HasDatabaseName("IX_products_featured")
                .HasFilter("deleted = 0");



            // Relationships
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.ProductImages)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ProductAttributeValues)
                .WithOne(pav => pav.Product)
                .HasForeignKey(pav => pav.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.ProductVariants)
                .WithOne(pv => pv.Product)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Inventories)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.CartItems)
                .WithOne(ci => ci.Product)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.ProductReviews)
                .WithOne(pr => pr.Product)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.WishlistItems)
                .WithOne(wi => wi.Product)
                .HasForeignKey(wi => wi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

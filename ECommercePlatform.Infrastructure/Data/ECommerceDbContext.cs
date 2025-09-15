using ECommercePlatform.Core.Entities;
using ECommercePlatform.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ECommercePlatform.Infrastructure.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantAttribute> ProductVariantAttributes { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserAddressConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeValueConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariantConfiguration());
            modelBuilder.ApplyConfiguration(new ProductVariantAttributeConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryConfiguration());
            modelBuilder.ApplyConfiguration(new ShoppingCartConfiguration());
            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new RefundConfiguration());
            modelBuilder.ApplyConfiguration(new ProductReviewConfiguration());
            modelBuilder.ApplyConfiguration(new WishlistConfiguration());
            modelBuilder.ApplyConfiguration(new WishlistItemConfiguration());
            modelBuilder.ApplyConfiguration(new ShippingMethodConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new EmailLogConfiguration());
            modelBuilder.ApplyConfiguration(new AdminUserConfiguration());
            modelBuilder.ApplyConfiguration(new SystemSettingConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());

            // Global query filters for soft delete
            // modelBuilder.Entity<User>().HasQueryFilter(u => !u.Deleted);
            // modelBuilder.Entity<UserAddress>().HasQueryFilter(ua => !ua.Deleted);
            // modelBuilder.Entity<Category>().HasQueryFilter(c => !c.Deleted);
            // modelBuilder.Entity<Brand>().HasQueryFilter(b => !b.Deleted);
            // modelBuilder.Entity<Product>().HasQueryFilter(p => !p.Deleted);
            // modelBuilder.Entity<ProductImage>().HasQueryFilter(pi => !pi.Deleted);
            // modelBuilder.Entity<ProductAttribute>().HasQueryFilter(pa => !pa.Deleted);
            // modelBuilder.Entity<ProductAttributeValue>().HasQueryFilter(pav => !pav.Deleted);
            // modelBuilder.Entity<ProductVariant>().HasQueryFilter(pv => !pv.Deleted);
            // modelBuilder.Entity<ProductVariantAttribute>().HasQueryFilter(pva => !pva.Deleted);
            // modelBuilder.Entity<Inventory>().HasQueryFilter(i => !i.Deleted);
            // modelBuilder.Entity<ShoppingCart>().HasQueryFilter(sc => !sc.Deleted);
            // modelBuilder.Entity<CartItem>().HasQueryFilter(ci => !ci.Deleted);
            // modelBuilder.Entity<Coupon>().HasQueryFilter(c => !c.Deleted);
            // modelBuilder.Entity<Order>().HasQueryFilter(o => !o.Deleted);
            // modelBuilder.Entity<OrderItem>().HasQueryFilter(oi => !oi.Deleted);
            // modelBuilder.Entity<Payment>().HasQueryFilter(p => !p.Deleted);
            // modelBuilder.Entity<Refund>().HasQueryFilter(r => !r.Deleted);
            // modelBuilder.Entity<ProductReview>().HasQueryFilter(pr => !pr.Deleted);
            // modelBuilder.Entity<Wishlist>().HasQueryFilter(w => !w.Deleted);
            // modelBuilder.Entity<WishlistItem>().HasQueryFilter(wi => !wi.Deleted);
            // modelBuilder.Entity<ShippingMethod>().HasQueryFilter(sm => !sm.Deleted);
            // modelBuilder.Entity<Notification>().HasQueryFilter(n => !n.Deleted);
            // modelBuilder.Entity<EmailLog>().HasQueryFilter(el => !el.Deleted);
            // modelBuilder.Entity<AdminUser>().HasQueryFilter(au => !au.Deleted);
            // modelBuilder.Entity<SystemSetting>().HasQueryFilter(ss => !ss.Deleted);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}

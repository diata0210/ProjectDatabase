using Microsoft.EntityFrameworkCore;

namespace ProjectDatabase.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ một-nhiều giữa User và Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.role_id);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Store)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.store_id);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.create_by);
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Membership)
                .WithMany(m => m.Customers)
                .HasForeignKey(c => c.membership_id);
            modelBuilder.Entity<Order>()
                .HasOne(ol => ol.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(ol => ol.customer_id);
            modelBuilder.Entity<Orderline>()
                .HasOne(ol => ol.Product)
                .WithMany(p => p.Orderlines)
                .HasForeignKey(ol => ol.product_id);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Store)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.store_id);
            modelBuilder.Entity<Store>()
                .HasOne(s => s.Province)
                .WithMany(p => p.Stores)
                .HasForeignKey(s => s.province_id);
            modelBuilder.Entity<Store>()
                .HasOne(s => s.District)
                .WithMany(p => p.Stores)
                .HasForeignKey(s => s.district_id);
            modelBuilder.Entity<District>()
                .HasOne(s => s.Province)
                .WithMany(p => p.Districts)
                .HasForeignKey(s => s.province_id);
            modelBuilder.Entity<Store_product>()
                .HasOne(s => s.Store)
                .WithMany(p => p.Store_products)
                .HasForeignKey(s => s.store_id);
            modelBuilder.Entity<Store_product>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Store_products)
                .HasForeignKey(s => s.product_id);
            modelBuilder.Entity<Product>()
                .HasOne(s => s.Product_type)
                .WithMany(p => p.Products)
                .HasForeignKey(s => s.type);
            modelBuilder.Entity<Order>()
                .HasOne(s => s.Order_type)
                .WithMany(p => p.Orders)
                .HasForeignKey(s => s.type);
            modelBuilder.Entity<Orderline>()
                .HasOne(o => o.Order)
                .WithMany(ol => ol.Orderlines)
                .HasForeignKey(o => o.order_id);
            modelBuilder.Entity<Revenue>()
                .HasOne(r => r.Store)
                .WithMany(s => s.Revenues)
                .HasForeignKey(r => r.store_id);
        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product>? Products { get; set; }

        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Order_type> Order_types { get; set; }
        public DbSet<Orderline> Orderlines { get; set; }
        public DbSet<Product_type> Product_Types { get; set; }
        public DbSet<Store_product> Store_products { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Revenue> Revenues { get; set; }




    }
}

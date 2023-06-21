using Microsoft.EntityFrameworkCore;
using ProjectDatabase.Models;

namespace ProjectDatabase.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
        public DbSet<User>? User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Store> Store { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ một-nhiều giữa User và Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.User)
                .HasForeignKey(u => u.role_id);
        }

        public DbSet<ProjectDatabase.Models.Product>? Product { get; set; }

        public DbSet<ProjectDatabase.Models.Customer>? Customer { get; set; }
        public DbSet<Order> ? Order { get; set; }
        public DbSet<Order_type> Order_type { get; set; }
        public DbSet<Orderline> Orderline { get; set; }
        public DbSet<Product_type> Product_Type { get; set; }
        public DbSet<Store_product> Store_product { get; set; }
        public DbSet<Membership> Membership { get; set; }

    }
}

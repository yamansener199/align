using align.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace align.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(nameof(User));

                entity.HasMany(a => a.Products)
                .WithOne(a => a.RegionManager)
                .HasForeignKey(a => a.RegionManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Products");

                entity.HasMany(a => a.Orders)
                .WithOne(a => a.RegionManager)
                .HasForeignKey(a => a.RegionManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Orders");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable(nameof(Product));

                entity.HasOne(a => a.RegionManager)
                .WithMany(a => a.Products)
                .HasForeignKey(a => a.RegionManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_RegionManager");

                entity.HasMany(a => a.Orders)
                .WithOne(a => a.Product)
                .HasForeignKey(a => a.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Orders");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable(nameof(Order));

                entity.HasOne(a => a.Product)
                .WithMany(a => a.Orders)
                .HasForeignKey(a => a.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Product");

                entity.HasOne(a => a.RegionManager)
                .WithMany(a => a.Orders)
                .HasForeignKey(a => a.RegionManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_RegionManager");
            });

            SeedRoles(modelBuilder);
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER" }
            );
        }

    }
}

using Microsoft.EntityFrameworkCore;
using ShoppingStore.ProductAPI.Models;

namespace ShoppingStore.ProductAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(150);

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Cuaderno", Price = 50.99, Description = "Esto es un cuaderno"},
                new Product { ProductId = 2, Name = "Computadora", Price = 20000.99, Description = "Esto es una computadora" }
            );
        }
    }
}

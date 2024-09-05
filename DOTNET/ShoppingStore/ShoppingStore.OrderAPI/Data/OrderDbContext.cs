using Microsoft.EntityFrameworkCore;
using ShoppingStore.OrderAPI.Models;

namespace ShoppingStore.OrderAPI.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {  }

        public DbSet<Order> Orders { get; set; }
    }
}

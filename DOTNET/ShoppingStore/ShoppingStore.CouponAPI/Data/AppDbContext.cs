using Microsoft.EntityFrameworkCore;
using ShoppingStore.CouponAPI.Models;

namespace ShoppingStore.CouponAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var coupons = new List<Coupon>()
            {
                new Coupon
                {
                    CouponId = 1,
                    CouponCode = "100AA",
                    DiscountAmount = 10,
                    MinAmount = 20,
                },
                new Coupon
                {
                    CouponId = 2,
                    CouponCode = "200AA",
                    DiscountAmount = 20,
                    MinAmount = 40,
                }
            };

            modelBuilder.Entity<Coupon>().HasData(coupons);
        }
    }
}

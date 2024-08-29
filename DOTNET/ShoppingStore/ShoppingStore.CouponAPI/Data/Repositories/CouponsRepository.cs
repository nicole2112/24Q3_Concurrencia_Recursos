using ShoppingStore.CouponAPI.Interfaces;
using ShoppingStore.CouponAPI.Models;
using System.Linq.Expressions;

namespace ShoppingStore.CouponAPI.Data.Repositories
{
    public class CouponsRepository : IRepository<Coupon>
    {
        private readonly AppDbContext _db;
        public CouponsRepository(AppDbContext db)
        {
            _db = db;
        }

        public Coupon Add(Coupon entity)
        {
            var entry = _db.Coupons.Add(entity);
            _db.SaveChanges();
            return entry.Entity;
        }

        public Coupon Delete(Coupon entity)
        {
            var entry = _db.Coupons.Remove(entity);
            _db.SaveChanges();
            return entry.Entity;
        }

        public Coupon Get(Expression<Func<Coupon, bool>> predicate)
        {
            return _db.Coupons.FirstOrDefault(predicate);
        }

        public Coupon Get(int id)
        {
            return _db.Coupons.FirstOrDefault(c => c.CouponId == id);
        }

        public IEnumerable<Coupon> GetAll()
        {
            return _db.Coupons;
        }

        public Coupon Update(Coupon entity)
        {
            var entry = _db.Coupons.Update(entity);
            _db.SaveChanges();
            return entry.Entity;
        }
    }
}

using ShoppingStore.CouponAPI.Interfaces;
using ShoppingStore.CouponAPI.Models;
using System.Linq.Expressions;

namespace ShoppingStore.CouponAPI.Data.Repositories
{
    public class CouponsRepository : IRepository<Coupon>
    {
        public Coupon Add(Coupon entity)
        {
            throw new NotImplementedException();
        }

        public Coupon Get(Expression<Func<Coupon, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Coupon Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Coupon> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

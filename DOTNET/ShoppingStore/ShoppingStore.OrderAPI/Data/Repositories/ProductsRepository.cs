using ShoppingStore.OrderAPI.Interfaces;
using ShoppingStore.OrderAPI.Models;
using System.Linq.Expressions;

namespace ShoppingStore.OrderAPI.Data.Repositories
{
    public class OrdersRepository : IRepository<Order>
    {
        private readonly OrderDbContext _db;
        public OrdersRepository(OrderDbContext db)
        {
            _db = db;
        }

        public Order Add(Order entity)
        {
            var entry = _db.Orders.Add(entity);
            _db.SaveChanges();
            return entry.Entity;
        }

        public Order Delete(Order entity)
        {
            var entry = _db.Orders.Remove(entity);
            _db.SaveChanges();
            return entry.Entity;
        }

        public Order Get(Expression<Func<Order, bool>> predicate)
        {
            return _db.Orders.FirstOrDefault(predicate);
        }

        public Order Get(int id)
        {
            return _db.Orders.FirstOrDefault(c => c.OrderId == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _db.Orders;
        }

        public Order Update(Order entity)
        {
            var entry = _db.Orders.Update(entity);
            _db.SaveChanges();
            return entry.Entity;
        }
    }
}

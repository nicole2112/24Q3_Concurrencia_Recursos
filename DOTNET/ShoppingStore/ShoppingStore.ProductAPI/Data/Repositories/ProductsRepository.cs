using ShoppingStore.ProductAPI.Interfaces;
using ShoppingStore.ProductAPI.Models;
using System.Linq.Expressions;

namespace ShoppingStore.ProductAPI.Data.Repositories
{
    public class ProductsRepository : IRepository<Product>
    {
        private readonly ProductDbContext _db;
        public ProductsRepository(ProductDbContext db)
        {
            _db = db;
        }

        public Product Add(Product entity)
        {
            var entry = _db.Products.Add(entity);
            _db.SaveChanges();
            return entry.Entity;
        }

        public Product Delete(Product entity)
        {
            var entry = _db.Products.Remove(entity);
            _db.SaveChanges();
            return entry.Entity;
        }

        public Product Get(Expression<Func<Product, bool>> predicate)
        {
            return _db.Products.FirstOrDefault(predicate);
        }

        public Product Get(int id)
        {
            return _db.Products.FirstOrDefault(c => c.ProductId == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        public Product Update(Product entity)
        {
            var entry = _db.Products.Update(entity);
            _db.SaveChanges();
            return entry.Entity;
        }
    }
}

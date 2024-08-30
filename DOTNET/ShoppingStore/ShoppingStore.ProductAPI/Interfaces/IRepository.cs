using System.Linq.Expressions;

namespace ShoppingStore.ProductAPI.Interfaces
{
    public interface IRepository<T> where T : class // constraint
    {
        IEnumerable<T> GetAll();

        T Get(Expression<Func<T, bool>> predicate);

        T Get(int id);

        T Add(T entity);

        T Update(T entity);

        T Delete(T entity);
    }
}

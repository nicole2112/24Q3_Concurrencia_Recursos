using ShoppingStore.OrderAPI.Models;

namespace ShoppingStore.OrderAPI.Interfaces
{
    public interface IOrderService
    {

        IEnumerable<Order> GetOrders();

        Task<Order> CreateOrderAsync(Order order);

        Task<Order> CancelOrderAsync(int orderId);
    }
}

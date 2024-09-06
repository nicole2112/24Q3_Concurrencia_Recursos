using ShoppingStore.OrderAPI.Models;

namespace ShoppingStore.OrderAPI.Interfaces
{
    public interface IOrderService
    {

        IEnumerable<Order> GetOrders();

        Task<Order> CreateOrderAsync(Order order);

        Order CancelOrder(int orderId);
    }
}

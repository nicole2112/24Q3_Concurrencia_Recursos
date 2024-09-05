using ShoppingStore.OrderAPI.Interfaces;
using ShoppingStore.OrderAPI.Models;

namespace ShoppingStore.OrderAPI.Services
{
    public class OrderService : IOrderService
    {

        private readonly IRepository<Order> _orderRepository;
        private readonly HttpClient _productHttpClient;
        private readonly HttpClient _couponHttpClient;
        public OrderService()
        {
            
        }

        public Task<Order> CancelOrderAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<Order> CreateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}

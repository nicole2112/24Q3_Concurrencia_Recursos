using Newtonsoft.Json;
using RabbitMQ.Client;
using ShoppingStore.OrderAPI.Interfaces;
using ShoppingStore.OrderAPI.Models;
using ShoppingStore.OrderAPI.Models.DTO;
using System.Text;

namespace ShoppingStore.OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private const string ProductApiUrl = "https://localhost:7002/api/";
        private const string CouponApiUrl = "https://localhost:7001/api/";
        private readonly IRepository<Order> _orderRepository;
        private readonly HttpClient _productHttpClient;
        private readonly HttpClient _couponHttpClient;
        public OrderService(HttpClient productHttpClient, HttpClient couponHttpClient, IRepository<Order> orderRepository)
        {
            _productHttpClient = productHttpClient;
            _couponHttpClient = couponHttpClient;
            _orderRepository = orderRepository;
            _productHttpClient.BaseAddress = new Uri(ProductApiUrl);
            _couponHttpClient.BaseAddress = new Uri(CouponApiUrl);
        }

        public Order CancelOrder(int orderId)
        {
            var order = _orderRepository.Get(orderId);

            if (order == null)
            {
                throw new Exception($"Orden con id {orderId} no fue encontrada.");
            }

            _orderRepository.Delete(order);

            SendNotification(new OrderInformation
            {
                OrderId = order.OrderId,
                Message = "Orden eliminada exitosamente",
                Address = "user@gmail.com"
            });

            return order;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // 1. cantidad es mayor a 0
            if (order.Quantity <= 0)
            {
                throw new Exception($"La cantidad debe ser mayor a 0.");
            }
            // 2. validar prod exista
            var responseString = await _productHttpClient.GetStringAsync($"products/{order.ProductId}");
            var productDto = JsonConvert.DeserializeObject<ResponseDTO<ProductDTO>>(responseString);
            if (productDto == null || !productDto.Success || productDto.Result == null)
            {
                throw new Exception($"Producto con id {order.ProductId} no fue encontrado.");
            }
            // 3. validar coupon exista y  el total >= al min amount
            responseString = await _couponHttpClient.GetStringAsync($"coupons/{order.CouponId}");
            var couponDto = JsonConvert.DeserializeObject<ResponseDTO<CouponDTO>>(responseString);
            if (couponDto != null && couponDto.Success && couponDto.Result != null)
            {
                var currentTotal = productDto.Result.Price * order.Quantity;
                if (currentTotal < couponDto.Result.MinAmount)
                {
                    throw new Exception($"Coupon con id {order.CouponId} requiere un total minimo de" +
                        $"{couponDto.Result.MinAmount}, total actual es {currentTotal}.");
                }
            }
            else if (order.CouponId > 0 && couponDto != null && couponDto.Success && couponDto.Result == null )
            {
                throw new Exception($"Coupon con id {order.CouponId} no fue encontrado.");
            }

            // 4. crear orden
            var res = _orderRepository.Update(order);

            SendNotification(new OrderInformation
            {
                OrderId = order.OrderId,
                Message = "Orden creada exitosamente",
                Address = "user@gmail.com"
            });

            return res;
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = _orderRepository.GetAll();
            return orders;
        }

        private static void SendNotification(OrderInformation orderInformation)
        {
            var json = JsonConvert.SerializeObject(orderInformation);
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("orders-queue", false, false, false, null);
                    var body = Encoding.UTF8.GetBytes(json);
                    channel.BasicPublish(string.Empty, "orders-queue", null, body);
                }
            }
        }
    }
}

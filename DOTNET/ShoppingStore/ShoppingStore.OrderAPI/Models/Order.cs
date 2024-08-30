namespace ShoppingStore.OrderAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int? CouponId { get; set; }

        public int Quantity { get; set; }
    }
}

namespace ShoppingStore.OrderAPI.Models.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int? CouponId { get; set; }

        public int Quantity { get; set; }
    }
}

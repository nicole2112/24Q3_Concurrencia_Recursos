namespace ShoppingStore.OrderAPI.Models.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public double Price { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}

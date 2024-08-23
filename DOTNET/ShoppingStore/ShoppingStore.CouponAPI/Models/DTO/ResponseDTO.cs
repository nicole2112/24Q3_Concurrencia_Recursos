namespace ShoppingStore.CouponAPI.Models.DTO
{
    public class ResponseDTO
    {
        public object? Result { get; set; }

        public bool Success { get; set; } = true;

        public string ErrorMessage { get; set; } = string.Empty;
    }
}

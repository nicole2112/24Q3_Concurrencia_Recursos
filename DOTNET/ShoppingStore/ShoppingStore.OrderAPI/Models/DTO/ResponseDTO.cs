namespace ShoppingStore.OrderAPI.Models.DTO
{
    public class ResponseDTO<T> where T : class
    {
        public T? Result { get; set; }

        public bool Success { get; set; } = true;

        public string ErrorMessage { get; set; } = string.Empty;
    }
}

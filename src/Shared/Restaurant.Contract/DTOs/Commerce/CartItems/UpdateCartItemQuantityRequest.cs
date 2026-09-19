namespace Restaurant.Contract.DTOs.Commerce.CartItems
{
    public class UpdateCartItemQuantityRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

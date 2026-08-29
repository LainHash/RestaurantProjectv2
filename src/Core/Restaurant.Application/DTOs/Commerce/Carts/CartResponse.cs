using Restaurant.Contract.DTOs.Commerce.CartItems;

namespace Restaurant.Contract.DTOs.Commerce.Carts
{
    public class CartResponse
    {
        public Guid Id { get; set; }

        public ICollection<CartItemResponse> CartItems { get; set; } = [];
    }
}

using Restaurant.Contract.DTOs.Commerce.WishlistItems;

namespace Restaurant.Contract.DTOs.Commerce.Wishlists
{
    public class WishlistResponse
    {
        public Guid Id { get; set; } 

        public ICollection<WishlistItemResponse> WishlistItems { get; set; } = [];
    }
}
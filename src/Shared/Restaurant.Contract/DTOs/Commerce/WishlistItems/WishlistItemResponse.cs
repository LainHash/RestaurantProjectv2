namespace Restaurant.Contract.DTOs.Commerce.WishlistItems
{
    public class WishlistItemResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string PrimaryImage { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = null!;
    }
}

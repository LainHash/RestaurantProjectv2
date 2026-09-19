namespace Restaurant.Contract.DTOs.Commerce.CartItems
{
    public class CartItemResponse
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string PrimaryImage { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; } = null!;
        public decimal LineTotal { get; set; }
    }
}

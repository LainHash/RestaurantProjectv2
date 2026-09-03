namespace Restaurant.Contract.DTOs.Sale.OrderDetails
{
    public class OrderDetailResponse
    {
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }

        public string? Note { get; set; }
    }
}

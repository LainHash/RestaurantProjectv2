namespace Restaurant.Contract.DTOs.Sale.OrderDetails
{
    public class CreateOrderDetailRequest
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public string? Note { get; set; }
    }
}

namespace Restaurant.Contract.DTOs.Sale.OrderDetails
{
    public class CreateOrderDetailRequest
    {
        public Guid ProductPublicId { get; set; }

        public int Quantity { get; set; }

        public string? Note { get; set; }
    }
}

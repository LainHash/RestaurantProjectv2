namespace Restaurant.Contract.DTOs.Sale.Orders
{
    public class CreateOrderFromCartRequest
    {
        public Guid BranchPublicId { get; set; }
        public string DeliveryAddress { get; set; } = null!;
        public string? Note { get; set; }
        public IEnumerable<Guid>? SelectedCartItemIds { get; set; }
    }
}

using Restaurant.Contract.DTOs.Sale.OrderDetails;

namespace Restaurant.Contract.DTOs.Sale.Orders
{
    public class AddOrderItemsRequest
    {
        public IEnumerable<CreateOrderDetailRequest> OrderDetails { get; set; } = [];
    }
}

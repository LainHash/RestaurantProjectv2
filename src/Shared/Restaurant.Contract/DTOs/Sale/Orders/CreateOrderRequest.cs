using Restaurant.Contract.DTOs.Sale.OrderDetails;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Sale.Orders
{
    public class CreateOrderRequest
    {
        public Guid? CustomerPublicId { get; set; }
        public Guid? EmployeePublicId { get; set; }
        public Guid BranchPublicId { get; set; }
        public Guid? RestaurantTablePublicId { get; set; }
        public string? DeliveryAddress { get; set; }

        public OrderType Type { get; set; }

        public string? Note { get; set; }

        public IEnumerable<CreateOrderDetailRequest> CreateOrderDetails { get; set; } = [];
    }
}

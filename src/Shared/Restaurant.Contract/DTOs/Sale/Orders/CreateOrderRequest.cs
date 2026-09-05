using Restaurant.Contract.DTOs.Sale.OrderDetails;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Sale.Orders
{
    public class CreateOrderRequest
    {
        public Guid? CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid BranchId { get; set; }

        public OrderType Type { get; set; }

        public string? Note { get; set; }

        public IEnumerable<CreateOrderDetailRequest> CreateOrderDetails { get; set; } = [];
    }
}

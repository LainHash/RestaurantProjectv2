using Restaurant.Contract.DTOs.Sale.OrderDetails;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Sale.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string OrderCode { get; set; } = null!;

        public string CustomerCode { get; set; } = null!;
        public string EmployeeCode { get; set; } = null!;
        public string BranchCode { get; set; } = null!;

        public OrderStatus Status { get; set; }
        public OrderType Type { get; set; }

        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }

        public string? Note { get; set; }

        public IEnumerable<OrderDetailResponse> OrderDetails { get; set; } = [];
    }
}

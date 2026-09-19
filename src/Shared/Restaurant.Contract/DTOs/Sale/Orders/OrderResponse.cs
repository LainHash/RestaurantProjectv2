using Restaurant.Contract.DTOs.Sale.OrderDetails;
using Restaurant.Domain.Enums;

namespace Restaurant.Contract.DTOs.Sale.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string OrderCode { get; set; } = null!;

        public string? CustomerCode { get; set; }
        public string? EmployeeCode { get; set; }
        public string BranchCode { get; set; } = null!;
        public Guid? RestaurantTableId { get; set; }
        public string? TableNumber { get; set; }
        public string? DeliveryAddress { get; set; }

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

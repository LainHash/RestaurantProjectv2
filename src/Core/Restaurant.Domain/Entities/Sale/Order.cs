using NanoidDotNet;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Sale
{
    public class Order : SoftDeletableEntity
    {
        public string OrderCode { get; private set; } = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 20);

        public int? CustomerId { get; private set; }
        public int EmployeeId { get; private set; }
        public int BranchId { get; private set; }

        public OrderStatus Status { get; private set; }
        public OrderType Type { get; private set; }

        public decimal DiscountAmount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal DeliveryFee { get; private set; }
        public decimal TotalAmount { get; private set; }

        public string? Note { get; private set; }

        public Customer Customer { get; private set; } = null!;
        public Employee Employee { get; private set; } = null!;
        public Branch Branch { get; private set; } = null!;
        public ICollection<OrderDetail> OrderDetails { get; private set; } = [];
    }
}

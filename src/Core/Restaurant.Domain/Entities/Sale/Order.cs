using NanoidDotNet;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Entities.Guest;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Sale
{
    public partial class Order : SoftDeletableEntity
    {
        public string OrderCode { get; private set; } = Nanoid.Generate("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", 20);

        public long? CustomerId { get; private set; }
        public long? EmployeeId { get; private set; }
        public long BranchId { get; private set; }
        public long? RestaurantTableId { get; private set; }

        public OrderStatus Status { get; private set; }
        public OrderType Type { get; private set; }

        public decimal Subtotal { get; private set; }
        public decimal DiscountAmount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal DeliveryFee { get; private set; }
        public decimal TotalAmount { get; private set; }

        public string? Note { get; private set; }
        public string? DeliveryAddress { get; private set; }

        public Customer? Customer { get; private set; } = null!;
        public Employee? Employee { get; private set; } = null!;
        public Branch Branch { get; private set; } = null!;
        public Invoice Invoice { get; private set; } = null!;
        public RestaurantTable? RestaurantTable { get; private set; } = null!;
        public ICollection<OrderDetail> OrderDetails { get; private set; } = [];
        public ICollection<OrderDiscount> OrderDiscounts { get; private set; } = [];
    }

    public partial class Order
    {
        public Order() { }

        public Order(
            long? customerId,
            long employeeId,
            long branchId)
        {
            CustomerId = customerId;
            EmployeeId = employeeId;
            BranchId = branchId;
            Status = OrderStatus.Pending;
        }

        public Order(
            long? customerId,
            long employeeId,
            long branchId,
            OrderType type,
            string? note)
            : this(customerId, employeeId, branchId)
        {
            Type = type;
            Note = note;
        }

        public static Order Create(
            long? customerId,
            long? employeeId,
            long branchId,
            long? restaurantTableId,
            OrderType type,
            string? note,
            string? deliveryAddress = null)
        {
            return new Order
            {
                CustomerId = customerId,
                EmployeeId = employeeId,
                BranchId = branchId,
                RestaurantTableId = restaurantTableId,
                Type = type,
                Note = note,
                DeliveryAddress = deliveryAddress,
                Status = OrderStatus.Pending
            };
        }

        public void Preparing()
        {
            Status = OrderStatus.Preparing;
        }

        public void Served()
        {
            Status = OrderStatus.Served;
        }

        public void Delivering()
        {
            Status = OrderStatus.Delivering;
        }

        public void Completed()
        {
            Status = OrderStatus.Completed;
        }

        public void Cancelled()
        {
            Status = OrderStatus.Cancelled;
        }

        public void CalculateSubtotal()
        {
            Subtotal = OrderDetails.Sum(x => x.LineTotal);
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = Subtotal - DiscountAmount + TaxAmount + DeliveryFee;
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            OrderDetails.Add(orderDetail);
            CalculateSubtotal();
        }

    }
}

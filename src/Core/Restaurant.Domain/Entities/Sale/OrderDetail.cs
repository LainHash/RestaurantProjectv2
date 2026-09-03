using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Sale
{
    public class OrderDetail : SoftDeletableEntity
    {
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }

        public string ProductName { get; private set; } = null!;
        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }
        public decimal TotalAmount { get; private set; }

        public string? Note { get; private set; }

        public Order Order { get; private set; } = null!;
        public Product Product { get; private set; } = null!;
    }
}

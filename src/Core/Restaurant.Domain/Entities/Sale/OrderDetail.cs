using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Sale
{
    public partial class OrderDetail : SoftDeletableEntity
    {
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }

        public string ProductName { get; private set; } = null!;
        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }
        public decimal LineTotal { get; private set; }

        public string? Note { get; private set; }

        public Order Order { get; private set; } = null!;
        public Product Product { get; private set; } = null!;
        public OrderPreparation OrderPreparation { get; private set; } = null!;
    }

    public partial class OrderDetail
    {
        public OrderDetail() { }

        public OrderDetail(
            int quantity,
            string? note)
        {
            Quantity = quantity;
            Note = note;
            OrderPreparation = new OrderPreparation();
        }

        public OrderDetail SetProduct(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            UnitPrice = product.ProductPrice.UnitPrice;
            return this;
        }

        public OrderDetail CalculateLineTotal()
        {
            LineTotal = UnitPrice * Quantity;
            return this;
        }
    }
}

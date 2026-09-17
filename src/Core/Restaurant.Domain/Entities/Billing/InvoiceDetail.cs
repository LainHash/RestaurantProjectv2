using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Billing
{
    public partial class InvoiceDetail : SoftDeletableEntity
    {
        public long InvoiceId { get; private set; }
        public long ProductId { get; private set; }

        public string ProductName { get; private set; } = null!;
        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }
        public decimal LineTotal { get; private set; }

        public Invoice Invoice { get; private set; } = null!;
        public Product Product { get; private set; } = null!;
    }

    public partial class InvoiceDetail
    {
        public InvoiceDetail() { }

        public InvoiceDetail(
            string productName,
            decimal unitPrice,
            int quantity,
            decimal lineTotal)
        {
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            LineTotal = lineTotal;
        }

        public InvoiceDetail(OrderDetail orderDetail)
            : this(orderDetail.ProductName,
                   orderDetail.UnitPrice,
                   orderDetail.Quantity,
                   orderDetail.LineTotal)
        {
            ProductId = orderDetail.ProductId;
        }
    }
}

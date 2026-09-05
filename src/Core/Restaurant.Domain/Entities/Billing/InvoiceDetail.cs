using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Models;

namespace Restaurant.Domain.Entities.Billing
{
    public class InvoiceDetail : SoftDeletableEntity
    {
        public int InvoiceId { get; private set; }
        public int ProductId { get; private set; }

        public string ProductName { get; private set; } = null!;
        public decimal UnitPrice { get; private set; }

        public int Quantity { get; private set; }
        public decimal LineTotal { get; private set; }

        public Invoice Invoice { get; private set; } = null!;
        public Product Product { get; private set; } = null!;
    }
}

using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Billing.Invoices.Commands.Checkout
{
    public class CheckoutOrderSpecification : BaseSpecification<Invoice>
    {
        public CheckoutOrderSpecification()
        {
            AddInclude(x => x.Order);
            AddIncludeAggregator(x => x.Include(i => i.InvoiceDetails)
                                        .ThenInclude(id => id.Product));
        }

        public void ApplyCriteria(long id)
        {
            AddCriteria(x => x.Id == id);
        }
    }
}

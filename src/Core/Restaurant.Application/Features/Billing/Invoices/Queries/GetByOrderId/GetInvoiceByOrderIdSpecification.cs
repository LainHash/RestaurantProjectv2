using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Billing.Invoices.Queries.GetByOrderId
{
    public class GetInvoiceByOrderIdSpecification
        : BaseSpecification<Invoice>
    {
        public GetInvoiceByOrderIdSpecification(GetInvoiceByOrderIdQuery query)
        {
            AddInclude(x => x.Order);
            AddIncludeAggregator(x => x.Include(i => i.InvoiceDetails)
                                        .ThenInclude(id => id.Product));

            AddCriteria(x => x.Order.PublicId == query.OrderId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Billing.Invoices.Queries.GetById
{
    public class GetInvoiceByIdSpecification
        : BaseSpecification<Invoice>
    {
        public GetInvoiceByIdSpecification(GetInvoiceByIdQuery query)
        {
            AddInclude(x => x.Order);
            AddIncludeAggregator(x => x.Include(i => i.InvoiceDetails)
                                        .ThenInclude(id => id.Product));

            AddCriteria(x => x.PublicId == query.Id);
        }
    }
}

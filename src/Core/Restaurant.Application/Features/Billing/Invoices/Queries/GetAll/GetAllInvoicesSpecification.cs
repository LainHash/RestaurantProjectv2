using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Billing;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Billing.Invoices.Queries.GetAll
{
    public class GetAllInvoicesSpecification
        : BaseSpecification<Invoice>
    {
        public GetAllInvoicesSpecification(GetAllInvoicesQuery query)
        {
            AddInclude(x => x.Order);

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(p =>
                    EF.Functions.Like(nameof(p.InvoiceCode), $"%{query.Keyword}%") ||
                    EF.Functions.Like(nameof(p.Status), $"%{query.Keyword}%"));
            }

            if (query.OrderCode is not null)
            {
                AddCriteria(p =>
                    p.Order.OrderCode == query.OrderCode);
            }

            switch (query.SortField)
            {
                case "price":
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.TotalAmount);
                    else
                        ApplyOrderByDescending(p => p.TotalAmount);
                    break;
                default:
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.CreatedAt);
                    else
                        ApplyOrderByDescending(p => p.CreatedAt);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Pricing;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Pricing.Discounts.Queries.GetAll
{
    public class GetAllDiscountsSpecification
        : BaseSpecification<Discount>
    {
        public GetAllDiscountsSpecification(GetAllDiscountsQuery query)
        {
            EnableSoftDeleteFilter();

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(p =>
                    EF.Functions.Like(p.Name, $"%{query.Keyword}%"));
            }

            switch (query.SortField)
            {
                case "name":
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.Name);
                    else
                        ApplyOrderByDescending(p => p.Name);
                    break;
                case "price":
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.MinimumOrderAmount ?? 0);
                    else
                        ApplyOrderByDescending(p => p.MinimumOrderAmount ?? 0);
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

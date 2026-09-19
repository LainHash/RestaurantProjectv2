using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetAll
{
    public class GetAllBrandsSpecification
        : BaseSpecification<Brand>
    {
        public GetAllBrandsSpecification(GetAllBrandsQuery query)
        {
            EnableSoftDeleteFilter();

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                Criteria = p =>
                    EF.Functions.Like(p.Name, $"%{query.Keyword}%") ||
                    EF.Functions.Like(p.Description, $"%{query.Keyword}%");
            }

            switch (query.SortField)
            {
                case "name":
                    if (query.IsAscending)
                        ApplyOrderBy(p => p.Name);
                    else
                        ApplyOrderByDescending(p => p.Name);
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

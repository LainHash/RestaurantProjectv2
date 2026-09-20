using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAllPopular
{
    public class GetAllPopularProductsSpecification
        : BaseSpecification<Product>
    {
        public GetAllPopularProductsSpecification(GetAllPopularProductsQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(p => p.ProductCategory);
            AddInclude(p => p.Unit);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.ProductPrice);
            AddIncludeAggregator(x => x.Include(p => p.ProductImages)
                                        .ThenInclude(pi => pi.Image));
            AddIncludeAggregator(x => x.Include(p => p.OrderDetails)
                                        .ThenInclude(od => od.Order));


            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                AddCriteria(p =>
                    EF.Functions.Like(p.Name, $"%{query.Keyword}%") ||
                    EF.Functions.Like(p.Description, $"%{query.Keyword}%"));
            }

            if (query.CategoryId is not null)
            {
                AddCriteria(p =>
                    p.ProductCategory.PublicId == query.CategoryId);
            }

            if (query.BrandId is not null)
            {
                AddCriteria(p =>
                    p.Brand!.PublicId == query.BrandId);
            }

            ApplyOrderByDescending(p => p.OrderDetails
                                        .Where(od => od.Order.Status == OrderStatus.Completed)
                                        .Sum(x => x.Quantity));

            switch (query.SortField)
            {
                case "name":
                    if (query.IsAscending)
                        ApplyThenBy(p => p.Name);
                    else
                        ApplyThenByDescending(p => p.Name);
                    break;
                case "price":
                    if (query.IsAscending)
                        ApplyThenBy(p => p.ProductPrice!.UnitPrice);
                    else
                        ApplyThenByDescending(p => p.ProductPrice!.UnitPrice);
                    break;
                default:
                    if (query.IsAscending)
                        ApplyThenBy(p => p.CreatedAt);
                    else
                        ApplyThenByDescending(p => p.CreatedAt);
                    break;
            }

            ApplyPaging(query.Page, query.PageSize);
        }
    }
}

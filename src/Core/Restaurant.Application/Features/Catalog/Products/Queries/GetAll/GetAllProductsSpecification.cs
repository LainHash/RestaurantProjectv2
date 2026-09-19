using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllProductsSpecification
        : BaseSpecification<Product>
    {
        public GetAllProductsSpecification(GetAllProductsQuery query)
        {
            EnableSoftDeleteFilter();

            AddInclude(p => p.ProductCategory);
            AddInclude(p => p.Unit);
            AddInclude(p => p.Brand!);
            AddInclude(p => p.ProductPrice);
            AddIncludeAggregator(x => x.Include(p => p.ProductImages)
                                        .ThenInclude((ProductImage pi) => pi.Image));


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
                        ApplyOrderBy(p => p.ProductPrice!.UnitPrice);
                    else
                        ApplyOrderByDescending(p => p.ProductPrice!.UnitPrice);
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

using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Queries.GetById
{
    public class GetProductCategoryByIdSpecification
        : BaseSpecification<ProductCategory>
    {
        public GetProductCategoryByIdSpecification(GetProductCategoryByIdQuery query)
        {
            Criteria = category => category.PublicId == query.Id;

            AddIncludeAggregator(x => x.Include(b => b.ProductCategoryImages)
                                        .ThenInclude(bi => bi.Image));

            EnableSoftDeleteFilter();
        }
    }
}

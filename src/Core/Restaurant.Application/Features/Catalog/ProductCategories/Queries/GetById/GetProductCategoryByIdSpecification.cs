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

            EnableSoftDeleteFilter();
        }
    }
}

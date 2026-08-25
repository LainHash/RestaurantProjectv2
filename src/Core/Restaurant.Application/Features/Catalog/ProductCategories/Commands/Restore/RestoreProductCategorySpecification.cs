using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Restore
{
    public class RestoreProductCategorySpecification
        : BaseSpecification<ProductCategory>
    {
        public RestoreProductCategorySpecification(RestoreProductCategoryCommand command)
        {
            Criteria = category => category.PublicId == command.Id;
        }
    }
}

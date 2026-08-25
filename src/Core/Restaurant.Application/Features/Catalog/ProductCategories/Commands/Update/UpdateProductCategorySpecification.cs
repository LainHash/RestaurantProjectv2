using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.ProductCategories.Commands.Update
{
    public class UpdateProductCategorySpecification
        : BaseSpecification<ProductCategory>
    {
        public UpdateProductCategorySpecification(UpdateProductCategoryCommand command)
        {
            Criteria = c => c.PublicId == command.Id;
        }
    }
}

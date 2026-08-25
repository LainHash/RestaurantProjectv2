using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Brands.Commands.Restore
{
    public class RestoreBrandSpecification
        : BaseSpecification<Brand>
    {
        public RestoreBrandSpecification(RestoreBrandCommand command)
        {
            Criteria = brand => brand.PublicId == command.Id;
        }
    }
}

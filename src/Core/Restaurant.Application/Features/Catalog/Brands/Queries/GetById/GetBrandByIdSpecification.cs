using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Catalog.Brands.Queries.GetById
{
    public class GetBrandByIdSpecification
        : BaseSpecification<Brand>
    {
        public GetBrandByIdSpecification(GetBrandByIdQuery query)
        {
            Criteria = brand => brand.PublicId == query.Id;

            AddIncludeAggregator(x => x.Include(b => b.BrandImages)
                                        .ThenInclude(bi => bi.Image));
            EnableSoftDeleteFilter();
        }
    }
}

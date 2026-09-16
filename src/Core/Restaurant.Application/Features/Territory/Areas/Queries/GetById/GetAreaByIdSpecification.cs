using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetById
{
    public class GetAreaByIdSpecification
        : BaseSpecification<Area>
    {
        public GetAreaByIdSpecification(GetAreaByIdQuery query)
        {
            EnableSoftDeleteFilter();

            AddCriteria(x => x.PublicId == query.Id);

            AddInclude(x => x.RestaurantTables);
        }
    }
}

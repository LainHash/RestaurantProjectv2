using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAllByAreaId
{
    public class GetAllRestaurantTableByAreaIdSpecification
        : BaseSpecification<RestaurantTable>
    {
        public GetAllRestaurantTableByAreaIdSpecification(GetAllRestaurantTableByAreaIdQuery query)
        {
            AddCriteria(x => x.Area.PublicId == query.AreaId);

            AddInclude(x => x.Area);
        }
    }
}

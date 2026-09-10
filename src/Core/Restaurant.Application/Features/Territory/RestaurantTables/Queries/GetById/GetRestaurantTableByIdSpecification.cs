using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetById
{
    public class GetRestaurantTableByIdSpecification
        : BaseSpecification<RestaurantTable>
    {
        public GetRestaurantTableByIdSpecification(GetRestaurantTableByIdQuery query)
        {
            AddCriteria(x => x.PublicId == query.Id);
        }
    }
}

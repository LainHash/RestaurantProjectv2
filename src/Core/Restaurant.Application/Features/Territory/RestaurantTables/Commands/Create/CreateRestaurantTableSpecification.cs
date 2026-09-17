using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Territory.RestaurantTables.Commands.Create
{
    public class CreateRestaurantTableSpecification
        : BaseSpecification<RestaurantTable>
    {
        public CreateRestaurantTableSpecification()
        {
        }

        public void ApplyCriteria(long id)
        {
            AddCriteria(x => x.Id == id);
        }
    }
}

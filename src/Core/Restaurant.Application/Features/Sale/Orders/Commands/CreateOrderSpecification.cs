using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Sale.Orders.Commands
{
    public class CreateOrderSpecification
        : BaseSpecification<Order>
    {
        public CreateOrderSpecification()
        {
            AddInclude(x => x.Customer);
            AddInclude(x => x.Employee);
            AddInclude(x => x.Branch);
            AddInclude(x => x.OrderDetails);
        }

        public void ApplyCriteria(int id)
        {
            AddCriteria(x => x.Id == id);
        }
    }
}

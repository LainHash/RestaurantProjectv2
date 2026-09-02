using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Sale.Orders.Queries.GetById
{
    public class GetOrderByIdSpecification
        : BaseSpecification<Order>
    {
        public GetOrderByIdSpecification(GetOrderByIdQuery query)
        {
            AddInclude(x => x.Customer);
            AddInclude(x => x.Employee);
            AddInclude(x => x.Branch);
            AddInclude(x => x.OrderDetails);

            AddCriteria(x => x.PublicId == query.Id);
        }
    }
}

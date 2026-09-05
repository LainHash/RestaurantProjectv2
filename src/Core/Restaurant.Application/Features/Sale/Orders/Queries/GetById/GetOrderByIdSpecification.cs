using Microsoft.EntityFrameworkCore;
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
            AddIncludeAggregator(x => x.Include(o => o.OrderDetails)
                                        .ThenInclude(od => od.OrderPreparation));

            AddCriteria(x => x.PublicId == query.Id);
        }
    }
}

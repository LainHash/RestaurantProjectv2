using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancel
{
    public class CancelOrderSpecification
        : BaseSpecification<OrderPreparation>
    {
        public CancelOrderSpecification(CancelOrderCommand command)
        {
            AddCriteria(x => x.OrderDetail.PublicId == command.OrderDetailId);

            AddIncludeAggregator(x => x.Include(op => op.OrderDetail)
                                        .ThenInclude(od => od.Order)
                                        .ThenInclude(o => o.OrderDetails)
                                        .ThenInclude(od => od.OrderPreparation));
        }
    }
}

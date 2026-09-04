using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Sale;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Sale.OrderPreparations.Commands.Preparing
{
    public class PreparingOrderSpecification
        : BaseSpecification<OrderPreparation>
    {
        public PreparingOrderSpecification(PreparingOrderCommand command)
        {
            AddCriteria(x => x.OrderDetail.PublicId == command.OrderDetailId);

            AddIncludeAggregator(x => x.Include(op => op.OrderDetail)
                                        .ThenInclude(od => od.Order));
        }
    }
}

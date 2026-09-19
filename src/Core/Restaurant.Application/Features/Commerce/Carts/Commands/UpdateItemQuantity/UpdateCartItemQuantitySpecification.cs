using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Restaurant.Application.Features.Commerce.Carts.Commands.UpdateItemQuantity
{
    public class UpdateCartItemQuantitySpecification
        : BaseSpecification<Cart>
    {
        public UpdateCartItemQuantitySpecification(UpdateCartItemQuantityCommand command)
        {
            if (command.UserId != null)
            {
                AddIncludeAggregator(x => x.Include(w => w.Customer!)
                                            .ThenInclude(c => c!.User));

                AddCriteria(x => x.Customer!.User.PublicId == command.UserId);
            }
            else
            {
                AddCriteria(x => x.SessionId == command.SessionId);
            }


            AddIncludeAggregator(x => x.Include(w => w.CartItems)
                                        .ThenInclude(wi => wi.Product)
                                        .ThenInclude(p => p.ProductPrice));
        }
    }
}

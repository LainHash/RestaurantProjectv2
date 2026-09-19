using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Commerce;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Features.Commerce.Wishlists.Queries.GetWishlist
{
    public class GetWishlistSpecification
        : BaseSpecification<Wishlist>
    {
        public GetWishlistSpecification(GetWishlistQuery query)
        {
            if (query.UserId != null)
            {
                AddIncludeAggregator(x => x.Include(w => w.Customer!)
                                            .ThenInclude(c => c!.User));

                AddCriteria(x => x.Customer!.User.PublicId == query.UserId);
            }
            else
            {
                AddCriteria(x => x.SessionId == query.SessionId);
            }

            AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                        .ThenInclude(wi => wi.Product)
                                        .ThenInclude(p => p.ProductPrice));

            AddIncludeAggregator(x => x.Include(w => w.WishlistItems)
                                        .ThenInclude(wi => wi.Product)
                                        .ThenInclude(p => p.ProductImages)
                                        .ThenInclude(pi => pi.Image));
        }
    }
}

using Restaurant.Domain.Entities.Storage;
using Restaurant.Domain.Repositories.Storage;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Storage
{
    internal class ImageRepository(RestaurantDbContext context) 
        : Repository<Image>(context), IImageRepository
    {
        private readonly RestaurantDbContext _context = context;
    }
}

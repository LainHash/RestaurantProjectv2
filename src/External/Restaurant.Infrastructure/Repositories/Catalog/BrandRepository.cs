using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Repositories.Catalog;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Catalog
{
    internal class BrandRepository(RestaurantDbContext context)
        : Repository<Brand>(context), IBrandRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Brands.AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<Brand?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Brands.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Brand?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Brands.FirstOrDefaultAsync(x => string.Equals(x.PublicId, id), cancellationToken);
        }

        public async Task<Brand?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Brands.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<IEnumerable<Brand>> ToListWithImagesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Brands
                .Include(b => b.BrandImages)
                    .ThenInclude(bi => bi.Image)
                .ToListAsync(cancellationToken);
        }
    }
}

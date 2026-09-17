using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Repositories.Personnel;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Personnel
{
    internal class DepartmentRepository(RestaurantDbContext context)
        : Repository<Department>(context), IDepartmentRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.AnyAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }

        public async Task<Department?> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Department?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<Department?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Name, name), cancellationToken);
        }
    }
}

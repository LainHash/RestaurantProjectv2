using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Repositories.Personnel;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Personnel
{
    internal class EmployeeRepository(RestaurantDbContext context) 
        : Repository<Employee>(context), IEmployeeRepository
    {
        private readonly RestaurantDbContext _context = context;

        public async Task<Employee?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Employee?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.PublicId == id, cancellationToken);
        }

        public async Task<Employee?> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }

        public async Task<Employee?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.User.PublicId == userId, cancellationToken);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities.Business;
using Restaurant.Domain.Repositories.Business;
using Restaurant.Infrastructure.Context;

namespace Restaurant.Infrastructure.Repositories.Business
{
    internal class AuditLogRepository : IAuditLogRepository
    {
        private readonly RestaurantDbContext _context;

        public AuditLogRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public void Add(AuditLog log) => _context.AuditLogs.Add(log);

        public void AddRange(IEnumerable<AuditLog> logs) => _context.AuditLogs.AddRange(logs);

        public async Task<(IEnumerable<AuditLog> Items, int TotalCount)> GetPagedAsync(
            string? entityName,
            int? userId,
            string? action,
            DateTime? from,
            DateTime? to,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _context.AuditLogs.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(entityName))
                query = query.Where(l => l.EntityName == entityName);

            if (userId.HasValue)
                query = query.Where(l => l.UserId == userId);

            if (!string.IsNullOrWhiteSpace(action))
                query = query.Where(l => l.Action == action);

            if (from.HasValue)
                query = query.Where(l => l.Timestamp >= from.Value);

            if (to.HasValue)
                query = query.Where(l => l.Timestamp <= to.Value);

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(l => l.Timestamp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public async Task PurgeOldLogsAsync(DateTime cutoff, CancellationToken cancellationToken = default)
        {
            await _context.AuditLogs
                .Where(l => l.Timestamp < cutoff)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}

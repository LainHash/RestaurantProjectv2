using Restaurant.Domain.Entities.Business;

namespace Restaurant.Domain.Repositories.Business
{
    public interface IAuditLogRepository
    {
        void Add(AuditLog log);
        void AddRange(IEnumerable<AuditLog> logs);

        Task<(IEnumerable<AuditLog> Items, int TotalCount)> GetPagedAsync(
            string? entityName,
            int? userId,
            string? action,
            DateTime? from,
            DateTime? to,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task PurgeOldLogsAsync(DateTime cutoff, CancellationToken cancellationToken = default);
    }
}

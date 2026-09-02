using Restaurant.Application.Services.Business;
using Restaurant.Contract.DTOs.Business.AuditLogs;
using Restaurant.Domain.Repositories.Business;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Infrastructure.Services.Business
{
    internal class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repository;

        public AuditLogService(IAuditLogRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<PageResult<IEnumerable<AuditLogResponse>>> GetPagedAsync(
            string? entityName,
            int? userId,
            string? action,
            DateTime? from,
            DateTime? to,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await _repository.GetPagedAsync(
                entityName, userId, action, from, to, page, pageSize, cancellationToken);

            var dtos = items.Select(l => new AuditLogResponse(
                l.Id,
                l.UserId,
                l.Action,
                l.EntityName,
                l.EntityId,
                l.OldValues,
                l.NewValues,
                l.IpAddress,
                l.Timestamp));

            return PageResult<IEnumerable<AuditLogResponse>>.Succeed(
                data: dtos,
                message: "Lấy audit log thành công.",
                totalItems: totalCount,
                skip: (page - 1) * pageSize,
                take: pageSize);
        }

        /// <inheritdoc />
        public async Task PurgeOldLogsAsync(int retentionDays, CancellationToken cancellationToken = default)
        {
            var cutoff = DateTime.UtcNow.AddDays(-retentionDays);
            await _repository.PurgeOldLogsAsync(cutoff, cancellationToken);
        }
    }
}

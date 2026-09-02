using Restaurant.Contract.DTOs.Business.AuditLogs;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Business
{
    /// <summary>
    /// Service quản lý audit log.
    /// </summary>
    public interface IAuditLogService
    {
        /// <summary>
        /// Lấy danh sách audit log theo filter và phân trang.
        /// </summary>
        Task<PageResult<IEnumerable<AuditLogResponse>>> GetPagedAsync(
            string? entityName,
            int? userId,
            string? action,
            DateTime? from,
            DateTime? to,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Xóa các audit log cũ hơn <paramref name="retentionDays"/> ngày.
        /// </summary>
        Task PurgeOldLogsAsync(int retentionDays, CancellationToken cancellationToken = default);
    }
}

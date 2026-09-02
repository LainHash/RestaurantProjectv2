using MediatR;
using Restaurant.Contract.DTOs.Business.AuditLogs;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Business.AuditLogs.Queries.GetAll
{
    /// <summary>
    /// Query lấy danh sách audit log với filter và phân trang.
    /// </summary>
    public record GetAllAuditLogsQuery : IRequest<PageResult<IEnumerable<AuditLogResponse>>>
    {
        /// <summary>Lọc theo tên entity (vd: "Product", "Order").</summary>
        public string? EntityName { get; init; }

        /// <summary>Lọc theo userId của người thực hiện.</summary>
        public int? UserId { get; init; }

        /// <summary>Lọc theo action: Created, Updated, Deleted.</summary>
        public string? Action { get; init; }

        /// <summary>Lọc từ thời điểm này (UTC).</summary>
        public DateTime? From { get; init; }

        /// <summary>Lọc đến thời điểm này (UTC).</summary>
        public DateTime? To { get; init; }

        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}

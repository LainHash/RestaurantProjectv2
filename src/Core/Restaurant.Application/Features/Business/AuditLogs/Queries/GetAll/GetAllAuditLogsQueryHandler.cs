using MediatR;
using Restaurant.Application.Services.Business;
using Restaurant.Contract.DTOs.Business.AuditLogs;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Business.AuditLogs.Queries.GetAll
{
    internal class GetAllAuditLogsQueryHandler(IAuditLogService auditLogService)
        : IRequestHandler<GetAllAuditLogsQuery, PageResult<IEnumerable<AuditLogResponse>>>
    {
        private readonly IAuditLogService _auditLogService = auditLogService;

        public async Task<PageResult<IEnumerable<AuditLogResponse>>> Handle(
            GetAllAuditLogsQuery request,
            CancellationToken cancellationToken)
        {
            return await _auditLogService.GetPagedAsync(
                entityName: request.EntityName,
                userId: request.UserId,
                action: request.Action,
                from: request.From,
                to: request.To,
                page: request.Page,
                pageSize: request.PageSize,
                cancellationToken: cancellationToken);
        }
    }
}

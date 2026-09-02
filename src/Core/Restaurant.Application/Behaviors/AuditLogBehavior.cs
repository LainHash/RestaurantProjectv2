using MediatR;
using Restaurant.Application.Services.Auth;

namespace Restaurant.Application.Behaviors
{
    /// <summary>
    /// MediatR Pipeline Behavior ghi nhận audit context (userId, IP) cho mỗi command.
    /// Thông tin này được lưu vào IAuditContext và DbContext sẽ đọc khi SaveChanges.
    /// </summary>
    public class AuditLogBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IAuditContext _auditContext;
        private readonly ICurrentUserService _currentUser;

        public AuditLogBehavior(
            IAuditContext auditContext,
            ICurrentUserService currentUser)
        {
            _auditContext = auditContext;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Truyền thông tin user vào AuditContext để DbContext dùng khi capture audit entries
            _auditContext.UserId = _currentUser.UserId;
            _auditContext.IpAddress = _currentUser.IpAddress;

            return await next();
        }
    }
}

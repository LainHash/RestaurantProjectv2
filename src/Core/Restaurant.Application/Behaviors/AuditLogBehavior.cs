using MediatR;
using Restaurant.Application.Services.Auth;
using Restaurant.Domain.Repositories.Identity;

namespace Restaurant.Application.Behaviors
{
    public class AuditLogBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IAuditContext _auditContext;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserRepository _userRepository;

        public AuditLogBehavior(
            IAuditContext auditContext,
            ICurrentUserService currentUser,
            IUserRepository userRepository)
        {
            _auditContext = auditContext;
            _currentUser = currentUser;
            _userRepository = userRepository;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            userId ??= Guid.Empty;

            var user = await _userRepository.FindByIdAsync(userId.Value, cancellationToken);
            if(user is null)
            {
                _auditContext.UserId = 0;
            }
            else
            {
                _auditContext.UserId = user.Id;
            }
            _auditContext.IpAddress = _currentUser.IpAddress;

            return await next(cancellationToken);
        }
    }
}

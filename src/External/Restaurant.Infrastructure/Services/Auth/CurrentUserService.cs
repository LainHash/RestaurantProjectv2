using Microsoft.AspNetCore.Http;
using Restaurant.Application.Services.Auth;
using System.Security.Claims;

namespace Restaurant.Infrastructure.Services.Auth
{
    /// <summary>
    /// Implementation lấy thông tin user hiện tại từ JWT claims và HTTP context.
    /// </summary>
    internal class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public int? UserId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User
                    ?.FindFirst(ClaimTypes.NameIdentifier)
                    ?? _httpContextAccessor.HttpContext?.User
                    ?.FindFirst("sub");

                if (claim is null) return null;

                return int.TryParse(claim.Value, out var id) ? id : null;
            }
        }

        /// <inheritdoc />
        public string? IpAddress =>
            _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }
}

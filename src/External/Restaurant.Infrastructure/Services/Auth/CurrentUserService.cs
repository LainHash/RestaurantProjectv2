using Microsoft.AspNetCore.Http;
using Restaurant.Application.Services.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurant.Infrastructure.Services.Auth
{
    internal class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User
                    ?.FindFirst(ClaimTypes.NameIdentifier)
                    ?? _httpContextAccessor.HttpContext?.User
                    ?.FindFirst(JwtRegisteredClaimNames.Sub);

                if (claim is null) return null;

                return Guid.TryParse(claim.Value, out var id) ? id : null;
            }
        }

        public string? IpAddress =>
            _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }
}

using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Contract.DTOs.Auth
{
    public class AuthenticationResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public AuthenticationResponse() { }

        public AuthenticationResponse(Guid userId, string userName, string email, string token)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Token = token;
        }

        public AuthenticationResponse(User user, string token)
            : this(user.PublicId, user.UserName, user.Email, token)
        {

        }
    }
}

namespace Restaurant.Application.Services.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(Guid userId, string userName, string email, string role);
        string GenerateRefreshToken();
        string HashToken(string rawToken);

        string GeneratePasswordResetToken(long userId);

        bool TryValidatePasswordResetToken(string token, out long userId);
    }
}

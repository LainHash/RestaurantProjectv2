namespace Restaurant.Application.Services.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(Guid userId, string userName, string email, string role);
        string GenerateRefreshToken();
        string HashToken(string rawToken);

        /// <summary>Generates a short-lived JWT (10 min) used exclusively for the reset-password step.</summary>
        string GeneratePasswordResetToken(int userId);

        /// <summary>
        /// Validates a password-reset JWT and extracts the user ID.
        /// Returns <c>false</c> if the token is invalid, expired, or not a password-reset token.
        /// </summary>
        bool TryValidatePasswordResetToken(string token, out int userId);
    }
}

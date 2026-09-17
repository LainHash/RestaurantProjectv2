using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Domain.Repositories.Identity
{
    public interface IUserRefreshTokenRepository : IRepository<UserRefreshToken>
    {
        Task<UserRefreshToken?> FindActiveByTokenHashAsync(
            string tokenHash,
            CancellationToken cancellationToken = default);

        Task RevokeAllByUserIdAsync(
            long userId,
            CancellationToken cancellationToken = default);
    }
}

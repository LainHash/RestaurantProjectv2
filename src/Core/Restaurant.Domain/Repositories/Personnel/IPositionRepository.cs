using Restaurant.Domain.Entities.Personnel;

namespace Restaurant.Domain.Repositories.Personnel
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<Position?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default);
    }
}

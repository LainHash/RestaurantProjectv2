using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Domain.Repositories.Inventory
{
    public interface IIngredientCategoryRepository : IRepository<IngredientCategory>
    {
        Task<IngredientCategory?> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<IngredientCategory?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IngredientCategory?> FindByNameAsync(string name, CancellationToken cancellationToken = default);

        Task<bool> IsExistingNameAsync(string name, CancellationToken cancellationToken = default);
    }
}

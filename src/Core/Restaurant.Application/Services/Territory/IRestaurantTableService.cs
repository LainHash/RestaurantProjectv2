using Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetAll;
using Restaurant.Application.Features.Territory.RestaurantTables.Queries.GetById;
using Restaurant.Contract.DTOs.Territory.RestaurantTables;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.Territory
{
    public interface IRestaurantTableService
    {
        Task<PageResult<IEnumerable<RestaurantTableResponse>>> GetAllAsync(
            GetAllRestaurantTablesSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<RestaurantTableResponse>> GetByIdAsync(
            GetRestaurantTableByIdSpecification specification,
            CancellationToken cancellationToken = default);
    }
}

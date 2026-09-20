using Restaurant.Application.Features.General.Homes.Queries.Get;
using Restaurant.Contract.DTOs.General;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Services.General
{
    public interface IHomeService
    {
        Task<Result<HomeResponse>> GetAsync(
            GetHomeQuery query,
            CancellationToken cancellationToken = default);
    }
}

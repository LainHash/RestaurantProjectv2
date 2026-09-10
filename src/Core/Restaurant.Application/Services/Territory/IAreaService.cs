using Restaurant.Application.Features.Territory.Areas.Commands.Create;
using Restaurant.Application.Features.Territory.Areas.Commands.Update;
using Restaurant.Application.Features.Territory.Areas.Queries.GetAll;
using Restaurant.Application.Features.Territory.Areas.Queries.GetAllByBranchId;
using Restaurant.Application.Features.Territory.Areas.Queries.GetById;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Territory
{
    public interface IAreaService
    {
        Task<PageResult<IEnumerable<AreaResponse>>> GetAllAsync(
            GetAllAreasSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<IEnumerable<AreaResponse>>> GetByBranchIdAsync(
            GetAllAreasByBranchIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<AreaResponse>> GetByIdAsync(
            GetAreaByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<AreaResponse>> CreateAsync(
            CreateAreaCommand command,
            CreateAreaSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<AreaResponse>> UpdateAsync(
            UpdateAreaCommand command,
            UpdateAreaSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            ISpecification<Area> specification,
            CancellationToken cancellationToken = default);

        Task<Result> RestoreAsync(
            ISpecification<Area> specification,
            CancellationToken cancellationToken = default);
    }
}

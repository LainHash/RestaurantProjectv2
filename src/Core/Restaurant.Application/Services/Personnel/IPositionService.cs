using Restaurant.Application.Features.Personnel.Positions.Commands.Create;
using Restaurant.Application.Features.Personnel.Positions.Commands.Update;
using Restaurant.Application.Features.Personnel.Positions.Queries.GetAll;
using Restaurant.Application.Features.Personnel.Positions.Queries.GetById;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Specifications;

namespace Restaurant.Application.Services.Personnel
{
    public interface IPositionService
    {
        Task<Result<IEnumerable<PositionResponse>>> GetAllAsync(
            GetAllPositionsSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<PositionResponse>> GetByIdAsync(
            GetPositionByIdSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result<PositionResponse>> CreateAsync(
            CreatePositionCommand command,
            CancellationToken cancellationToken = default);

        Task<Result<PositionResponse>> UpdateAsync(
            UpdatePositionCommand command,
            UpdatePositionSpecification specification,
            CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(
            ISpecification<Position> specification,
            CancellationToken cancellationToken = default);

        Task<Result> RestoreAsync(
            ISpecification<Position> specification,
            CancellationToken cancellationToken = default);
    }
}

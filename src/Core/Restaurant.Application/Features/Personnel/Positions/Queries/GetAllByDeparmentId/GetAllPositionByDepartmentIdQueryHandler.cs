using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetAllByDeparmentId
{
    internal class GetAllPositionByDepartmentIdQueryHandler(IPositionService positionService)
                : IRequestHandler<GetAllPositionByDepartmentIdQuery, Result<IEnumerable<PositionResponse>>>
    {
        private readonly IPositionService _positionService = positionService;

        public async Task<Result<IEnumerable<PositionResponse>>> Handle(GetAllPositionByDepartmentIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllPositionByDepartmentIdSpecification(request);
            var response = await _positionService.GetAllByDepartmentIdAsync(specification, cancellationToken);
            return response;
        }
    }
}

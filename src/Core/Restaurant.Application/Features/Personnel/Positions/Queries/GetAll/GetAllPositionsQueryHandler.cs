using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetAll
{
    internal class GetAllPositionsQueryHandler(IPositionService positionService)
                  : IRequestHandler<GetAllPositionsQuery, Result<IEnumerable<PositionResponse>>>
    {
        private readonly IPositionService _positionService = positionService;

        public async Task<Result<IEnumerable<PositionResponse>>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllPositionsSpecification(request);
            var response = await _positionService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}

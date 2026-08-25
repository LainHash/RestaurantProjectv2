using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetById
{
    internal class GetPositionByIdQueryHandler(IPositionService positionService)
                : IRequestHandler<GetPositionByIdQuery, Result<PositionResponse>>
    {
        private readonly IPositionService _positionService = positionService;

        public async Task<Result<PositionResponse>> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetPositionByIdSpecification(request);
            var response = await _positionService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}

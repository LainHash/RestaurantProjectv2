using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Queries.GetByName
{
    internal class GetPositionByNameQueryHandler(IPositionService positionService)
                : IRequestHandler<GetPositionByNameQuery, Result<PositionResponse>>
    {
        private readonly IPositionService _positionService = positionService;

        public async Task<Result<PositionResponse>> Handle(GetPositionByNameQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetPositionByNameSpecification(request);
            var response = await _positionService.GetByNameAsync(specification, cancellationToken);
            return response;
        }
    }
}

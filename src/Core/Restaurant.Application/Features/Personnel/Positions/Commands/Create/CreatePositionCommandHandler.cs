using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Create
{
    internal class CreatePositionCommandHandler(IPositionService positionService)
                : IRequestHandler<CreatePositionCommand, Result<PositionResponse>>
    {
        private readonly IPositionService _positionService = positionService;

        public async Task<Result<PositionResponse>> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            var response = await _positionService.CreateAsync(request, cancellationToken);
            return response;
        }
    }
}

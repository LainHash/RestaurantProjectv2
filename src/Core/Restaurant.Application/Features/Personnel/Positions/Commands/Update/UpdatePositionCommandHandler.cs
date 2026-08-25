using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Update
{
    internal class UpdatePositionCommandHandler(IPositionService positionService)
                : IRequestHandler<UpdatePositionCommand, Result<PositionResponse>>
    {
        private readonly IPositionService _positionService = positionService;

        public async Task<Result<PositionResponse>> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdatePositionSpecification(request);
            var response = await _positionService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}

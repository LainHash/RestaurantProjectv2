using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Restore
{
    internal class RestorePositionCommandHandler(IPositionService positionService)
                : IRequestHandler<RestorePositionCommand, Result>
    {
        private readonly IPositionService _positionService = positionService;

        public async Task<Result> Handle(RestorePositionCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestorePositionSpecification(request);
            var response = await _positionService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}

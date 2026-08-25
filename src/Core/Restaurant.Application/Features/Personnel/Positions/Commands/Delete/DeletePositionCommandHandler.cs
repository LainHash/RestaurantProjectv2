using MediatR;
using Restaurant.Application.Services.Personnel;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Personnel.Positions.Commands.Delete
{
    internal class DeletePositionCommandHandler(IPositionService positionService)
                : IRequestHandler<DeletePositionCommand, Result>
    {
        private readonly IPositionService _positionService = positionService;

        public async Task<Result> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            var specification = new DeletePositionSpecification(request);
            var response = await _positionService.DeleteAsync(specification, cancellationToken);
            return response;
        }
    }
}

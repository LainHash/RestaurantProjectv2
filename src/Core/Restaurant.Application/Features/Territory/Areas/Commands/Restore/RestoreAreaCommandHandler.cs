using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Restore
{
    internal class RestoreAreaCommandHandler(IAreaService areaService)
        : IRequestHandler<RestoreAreaCommand, Result>
    {
        private readonly IAreaService _areaService = areaService;

        public async Task<Result> Handle(RestoreAreaCommand request, CancellationToken cancellationToken)
        {
            var specification = new RestoreAreaSpecification(request);
            var response = await _areaService.RestoreAsync(specification, cancellationToken);
            return response;
        }
    }
}

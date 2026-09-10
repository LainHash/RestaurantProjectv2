using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Update
{
    internal class UpdateAreaCommandHandler(IAreaService areaService)
        : IRequestHandler<UpdateAreaCommand, Result<AreaResponse>>
    {
        private readonly IAreaService _areaService = areaService;

        public async Task<Result<AreaResponse>> Handle(UpdateAreaCommand request, CancellationToken cancellationToken)
        {
            var specification = new UpdateAreaSpecification(request);
            var response = await _areaService.UpdateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}

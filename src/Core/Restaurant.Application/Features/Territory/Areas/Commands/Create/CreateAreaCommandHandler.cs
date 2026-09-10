using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Commands.Create
{
    internal class CreateAreaCommandHandler(IAreaService areaService)
        : IRequestHandler<CreateAreaCommand, Result<AreaResponse>>
    {
        private readonly IAreaService _areaService = areaService;

        public async Task<Result<AreaResponse>> Handle(CreateAreaCommand request, CancellationToken cancellationToken)
        {
            var specification = new CreateAreaSpecification();
            var response = await _areaService.CreateAsync(request, specification, cancellationToken);
            return response;
        }
    }
}

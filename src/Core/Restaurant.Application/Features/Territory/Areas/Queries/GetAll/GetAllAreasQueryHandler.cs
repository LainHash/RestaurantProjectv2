using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAll
{
    internal class GetAllAreasQueryHandler(IAreaService areaService)
        : IRequestHandler<GetAllAreasQuery, PageResult<IEnumerable<AreaResponse>>>
    {
        private readonly IAreaService _areaService = areaService;

        public async Task<PageResult<IEnumerable<AreaResponse>>> Handle(GetAllAreasQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllAreasSpecification(request);
            var response = await _areaService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}

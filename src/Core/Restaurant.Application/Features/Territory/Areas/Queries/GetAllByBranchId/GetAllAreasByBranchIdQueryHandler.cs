using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetAllByBranchId
{
    internal class GetAllAreasByBranchIdQueryHandler(IAreaService areaService)
                : IRequestHandler<GetAllAreasByBranchIdQuery, Result<IEnumerable<AreaResponse>>>
    {
        private readonly IAreaService _areaService = areaService;

        public async Task<Result<IEnumerable<AreaResponse>>> Handle(GetAllAreasByBranchIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllAreasByBranchIdSpecification(request);
            var response = await _areaService.GetByBranchIdAsync(specification, cancellationToken);
            return response;
        }
    }
}

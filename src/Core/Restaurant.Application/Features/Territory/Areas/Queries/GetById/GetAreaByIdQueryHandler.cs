using MediatR;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Territory.Areas.Queries.GetById
{
    internal class GetAreaByIdQueryHandler(IAreaService areaService)
        : IRequestHandler<GetAreaByIdQuery, Result<AreaDetailResponse>>
    {
        private readonly IAreaService _areaService = areaService;

        public async Task<Result<AreaDetailResponse>> Handle(GetAreaByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAreaByIdSpecification(request);
            var response = await _areaService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}

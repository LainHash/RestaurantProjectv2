using MediatR;
using Restaurant.Application.Services.General;
using Restaurant.Contract.DTOs.General;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.General.Homes.Queries.Get
{
    internal class GetHomeQueryHandler(IHomeService homeService)
                : IRequestHandler<GetHomeQuery, Result<HomeResponse>>
    {
        private readonly IHomeService _homeService = homeService;

        public async Task<Result<HomeResponse>> Handle(GetHomeQuery request, CancellationToken cancellationToken)
        {
            var response = await _homeService.GetAsync(request, cancellationToken);
            return response;
        }
    }
}

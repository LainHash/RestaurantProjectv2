using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Identity.Users;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Users.Queries.GetById
{
    internal class GetUserByIdQueryHandler(IUserService userService)
                : IRequestHandler<GetUserByIdQuery, Result<UserDetailResponse>>
    {
        private readonly IUserService _userService = userService;

        public async Task<Result<UserDetailResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetUserByIdSpecification(request);
            var response = await _userService.GetByIdAsync(specification, cancellationToken);
            return response;
        }
    }
}

using MediatR;
using Restaurant.Application.Services.Identity;
using Restaurant.Contract.DTOs.Identity.Users;
using Restaurant.Domain.Models.Results;

namespace Restaurant.Application.Features.Identity.Users.Queries.GetAll
{
    internal class GetAllUsersQueryHandler(IUserService userService)
                : IRequestHandler<GetAllUsersQuery, PageResult<IEnumerable<UserResponse>>>
    {
        private readonly IUserService _userService = userService;

        public async Task<PageResult<IEnumerable<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetAllUsersSpecification(request);
            var response = await _userService.GetAllAsync(specification, cancellationToken);
            return response;
        }
    }
}

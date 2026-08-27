using AutoMapper;
using Restaurant.Application.Features.Identity.Users.Commands.CreateForEmployee;
using Restaurant.Application.Services.Auth;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Identity;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Identity;
using System.Net;
using System.Resources;

namespace Restaurant.Infrastructure.Services.Identity
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        private readonly IPasswordHasher _passwordHasher;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateForEmployeeAsync(
            CreateUsersForEmployeeCommand command,
            CancellationToken cancellationToken)
        {
            var employeeRole = await _roleRepository.FindByNameAsync("Employee", cancellationToken);

            var index = 1;
            for (; index <= command.Body.Quantity; index++)
            {
                var code = command.Body.BatchNumber.ToString("00")
                            + DateTime.UtcNow.Month.ToString("00")
                            + index.ToString("000000");

                var email = code + "@HauteDeHallen.edu.vn";

                var passwordHash = _passwordHasher.HashPassword(code);

                var user = User.CreateForEmployee(code, email, passwordHash, employeeRole!.Id);

                _userRepository.Add(user);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return Result
                .Succeed($"Create {index} employee account successfully.", HttpStatusCode.Created);
        }
    }
}

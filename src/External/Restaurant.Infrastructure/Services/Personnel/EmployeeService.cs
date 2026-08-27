using AutoMapper;
using Restaurant.Application.Features.Personnel.Employees.Commands.Create;
using Restaurant.Application.Features.Personnel.Employees.Queries.GetAll;
using Restaurant.Application.Features.Personnel.Employees.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Entities.Identity;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Identity;
using Restaurant.Domain.Repositories.Personnel;
using Restaurant.Domain.Repositories.Territory;
using System.Net;

namespace Restaurant.Infrastructure.Services.Personnel
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IPersonalProfileRepository _personalProfileRepository;
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IPersonalProfileRepository personalProfileRepository,
            IUserRepository userRepository,
            IPositionRepository positionRepository,
            IBranchRepository branchRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _personalProfileRepository = personalProfileRepository;
            _userRepository = userRepository;
            _positionRepository = positionRepository;
            _branchRepository = branchRepository;
        }

        public async Task<Result<IEnumerable<EmployeeResponse>>> GetAllAsync(
            GetAllEmployeesSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var employees = await _employeeRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<EmployeeResponse>>(employees);
            return Result<IEnumerable<EmployeeResponse>>
                .Succeed(response, Success<Employee>.Retrieved);
        }

        public async Task<Result<EmployeeResponse>> GetByIdAsync(
            GetEmployeeByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.FindAsync(specification, cancellationToken);
            if (employee is null)
            {
                return Result<EmployeeResponse>
                    .Fail(Error<Employee>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<EmployeeResponse>(employee);
            return Result<EmployeeResponse>
                .Succeed(response, Success<Employee>.Retrieved);
        }

        public async Task<Result<EmployeeResponse>> CreateAsync(
            CreateEmployeeCommand command,
            CreateEmployeeSpecification specification,
            CancellationToken cancellationToken = default)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var user = await _userRepository.FindByIdAsync(command.Body.UserId, cancellationToken);
                if(user is null)
                {
                    return Result<EmployeeResponse>
                        .Fail(Error<User>.NotFound, HttpStatusCode.NotFound);
                }

                var position = await _positionRepository.FindByIdAsync(command.Body.PositionId, cancellationToken);
                if(position is null)
                {
                    return Result<EmployeeResponse>
                        .Fail(Error<Position>.NotFound, HttpStatusCode.NotFound);
                }

                var branch = await _branchRepository.FindByIdAsync(command.Body.BranchId, cancellationToken);
                if(branch is null)
                {
                    return Result<EmployeeResponse>
                        .Fail(Error<Branch>.NotFound, HttpStatusCode.NotFound);
                }

                var employee = _mapper.Map<Employee>(command.Body)
                    .SetUser(user.Id)
                    .SetPosition(position.Id)
                    .SetBranch(branch.Id);
                _employeeRepository.Add(employee);

                var personalProfile = _mapper.Map<PersonalProfile>(command.Body.CreatePersonalProfileRequest)
                    .SetUser(user.Id);
                _personalProfileRepository.Add(personalProfile);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                var createdEmployee = await _employeeRepository.FindAsync(specification, cancellationToken);
                var response = _mapper.Map<EmployeeResponse>(createdEmployee);
                return Result<EmployeeResponse>
                    .Succeed(response, Success<Employee>.Created, HttpStatusCode.Created);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);

                return Result<EmployeeResponse>
                        .Fail("Create employee request failed.", HttpStatusCode.InternalServerError);
            }
        }
    }
}

using AutoMapper;
using Restaurant.Application.Features.Personnel.Employees.Commands.Create;
using Restaurant.Application.Features.Personnel.Employees.Queries.GetAll;
using Restaurant.Application.Features.Personnel.Employees.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Employees;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Personnel;
using System.Net;

namespace Restaurant.Infrastructure.Services.Personnel
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            if(employee is null)
            {
                return Result<EmployeeResponse>
                    .Fail(Error<Employee>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<EmployeeResponse>(employee);
            return Result<EmployeeResponse>
                .Succeed(response, Success<Employee>.Retrieved);
        }

        public Task<Result<EmployeeResponse>> CreateAsync(
            CreateEmployeeCommand command,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

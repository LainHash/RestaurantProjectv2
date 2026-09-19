using AutoMapper;
using Restaurant.Application.Features.Personnel.Positions.Commands.Create;
using Restaurant.Application.Features.Personnel.Positions.Commands.Update;
using Restaurant.Application.Features.Personnel.Positions.Queries.GetAll;
using Restaurant.Application.Features.Personnel.Positions.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Personnel;
using Restaurant.Contract.DTOs.Personnel.Positions;
using Restaurant.Domain.Entities.Personnel;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Personnel;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Infrastructure.Services.Personnel
{
    internal class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IDepartmentRepository _departmentRepository;

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PositionService(
            IPositionRepository positionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IDepartmentRepository departmentRepository)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;
        }

        public async Task<Result<IEnumerable<PositionResponse>>> GetAllAsync(
            GetAllPositionsSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var positions = await _positionRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<PositionResponse>>(positions);
            return Result<IEnumerable<PositionResponse>>
                .Succeed(response, Success.Retrieved("Position"));
        }

        public async Task<Result<PositionResponse>> GetByIdAsync(
            GetPositionByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var position = await _positionRepository.FindAsync(specification, cancellationToken);
            if(position is null)
            {
                return Result<PositionResponse>
                    .Fail(Error.NotFound("Position"), HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<PositionResponse>(position);
            return Result<PositionResponse>
                .Succeed(response, Success.Retrieved("Position"));
        }

        public async Task<Result<PositionResponse>> CreateAsync(
            CreatePositionCommand command,
            CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.FindByIdAsync(command.Body.DepartmentId, cancellationToken);
            if (department is null)
            {
                return Result<PositionResponse>
                    .Fail(Error.NotFound("Department"), HttpStatusCode.NotFound);
            }

            var existing = await _positionRepository.FindByNameAsync(command.Body.Name, cancellationToken);
            if (existing is not null)
            {
                return Result<PositionResponse>
                    .Fail(Error.ExistedName("Position"), HttpStatusCode.Conflict);
            }

            var position = _mapper.Map<Position>(command.Body);
            position.SetDepartment(department.Id);

            _positionRepository.Add(position);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Re-load with department for response mapping
            var created = await _positionRepository.FindAsync(
                new GetPositionByIdSpecification(new(position.PublicId)), cancellationToken);

            var response = _mapper.Map<PositionResponse>(created);
            return Result<PositionResponse>
                .Succeed(response, Success.Created("Position"), HttpStatusCode.Created);
        }

        public async Task<Result<PositionResponse>> UpdateAsync(
            UpdatePositionCommand command,
            UpdatePositionSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var position = await _positionRepository.FindAsync(specification, cancellationToken);
            if (position is null)
            {
                return Result<PositionResponse>
                    .Fail(Error.NotFound("Position"), HttpStatusCode.NotFound);
            }

            // Validate name uniqueness (allow keeping same name on self)
            var nameExists = await _positionRepository.IsExistingNameAsync(command.Body.Name, cancellationToken);
            if (nameExists && !string.Equals(position.Name, command.Body.Name, StringComparison.OrdinalIgnoreCase))
            {
                return Result<PositionResponse>
                    .Fail(Error.ExistedName("Position"), HttpStatusCode.Conflict);
            }

            var department = await _departmentRepository.FindByIdAsync(command.Body.DepartmentId, cancellationToken);
            if (department is null)
            {
                return Result<PositionResponse>
                    .Fail(Error.NotFound("Department"), HttpStatusCode.NotFound);
            }

            _mapper.Map(command.Body, position);
            position.SetDepartment(department.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Re-load with department for response mapping
            var updated = await _positionRepository.FindAsync(
                new GetPositionByIdSpecification(new(position.PublicId)), cancellationToken);

            var response = _mapper.Map<PositionResponse>(updated);
            return Result<PositionResponse>
                .Succeed(response, Success.Updated("Position"), HttpStatusCode.OK);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Position> specification,
            CancellationToken cancellationToken = default)
        {
            var position = await _positionRepository.FindAsync(specification, cancellationToken);
            if (position is null)
            {
                return Result
                    .Fail(Error.NotFound("Position"), HttpStatusCode.NotFound);
            }

            if (position.IsDeleted)
            {
                return Result
                    .Fail(Error.AlreadyDeleted("Position"), HttpStatusCode.BadRequest);
            }

            position.SoftDelete();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Deleted("Position"));
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Position> specification,
            CancellationToken cancellationToken = default)
        {
            var position = await _positionRepository.FindAsync(specification, cancellationToken);
            if (position is null)
            {
                return Result
                    .Fail(Error.NotFound("Position"), HttpStatusCode.NotFound);
            }

            if (!position.IsDeleted)
            {
                return Result
                    .Fail(Error.NotYetDeleted("Position"), HttpStatusCode.BadRequest);
            }

            position.Restore();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result
                .Succeed(Success.Restored("Position"));
        }
    }
}

using AutoMapper;
using Restaurant.Application.Features.Territory.Areas.Commands.Create;
using Restaurant.Application.Features.Territory.Areas.Commands.Update;
using Restaurant.Application.Features.Territory.Areas.Queries.GetAll;
using Restaurant.Application.Features.Territory.Areas.Queries.GetById;
using Restaurant.Application.Services.Business;
using Restaurant.Application.Services.Territory;
using Restaurant.Contract.DTOs.Territory.Areas;
using Restaurant.Domain.Entities.Territory;
using Restaurant.Domain.Models.Messages;
using Restaurant.Domain.Models.Results;
using Restaurant.Domain.Repositories.Territory;
using Restaurant.Domain.Specifications;
using System.Net;

namespace Restaurant.Infrastructure.Services.Territory
{
    internal class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AreaService(
            IAreaRepository areaRepository,
            IBranchRepository branchRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _areaRepository = areaRepository;
            _branchRepository = branchRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PageResult<IEnumerable<AreaResponse>>> GetAllAsync(
            GetAllAreasSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await _areaRepository.CountAsync(specification, cancellationToken);
            var areas = await _areaRepository.ToListAsync(specification, cancellationToken);

            var response = _mapper.Map<IEnumerable<AreaResponse>>(areas);
            return PageResult<IEnumerable<AreaResponse>>
                .Succeed(response, Success<Area>.Retrieved, totalItems, specification.Skip, specification.Take);
        }

        public async Task<Result<AreaDetailResponse>> GetByIdAsync(
            GetAreaByIdSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var area = await _areaRepository.FindAsync(specification, cancellationToken);
            if (area is null)
            {
                return Result<AreaDetailResponse>
                    .Fail(Error<Area>.NotFound, HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<AreaDetailResponse>(area);
            return Result<AreaDetailResponse>
                .Succeed(response, Success<Area>.Retrieved);
        }

        public async Task<Result<AreaResponse>> CreateAsync(
            CreateAreaCommand command,
            CreateAreaSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var branch = await _branchRepository.FindByIdAsync(command.Body.BranchId, cancellationToken);
            if (branch is null)
            {
                return Result<AreaResponse>
                    .Fail(Error<Branch>.NotFound, HttpStatusCode.NotFound);
            }

            if (await _areaRepository.IsExistingNameAsync(branch.Id, command.Body.Name, cancellationToken))
            {
                return Result<AreaResponse>
                    .Fail(Error<Area>.ExistedName, HttpStatusCode.Conflict);
            }

            var area = _mapper.Map<Area>(command.Body)
                .SetBranch(branch.Id);

            _areaRepository.Add(area);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            specification.ApplyCriteria(area.Id);
            var createdArea = await _areaRepository.FindAsync(specification, cancellationToken);

            var response = _mapper.Map<AreaResponse>(createdArea);
            return Result<AreaResponse>
                .Succeed(response, Success<Area>.Created, HttpStatusCode.Created);
        }

        public async Task<Result<AreaResponse>> UpdateAsync(
            UpdateAreaCommand command,
            UpdateAreaSpecification specification,
            CancellationToken cancellationToken = default)
        {
            var branch = await _branchRepository.FindByIdAsync(command.Body.BranchId, cancellationToken);
            if (branch is null)
            {
                return Result<AreaResponse>
                    .Fail(Error<Branch>.NotFound, HttpStatusCode.NotFound);
            }

            var area = await _areaRepository.FindAsync(specification, cancellationToken);
            if (area is null)
            {
                return Result<AreaResponse>
                    .Fail(Error<Area>.NotFound, HttpStatusCode.NotFound);
            }

            var nameExists = await _areaRepository.IsExistingNameAsync(branch.Id, command.Body.Name, cancellationToken);
            if (nameExists && !string.Equals(area.Name, command.Body.Name, StringComparison.OrdinalIgnoreCase))
            {
                return Result<AreaResponse>
                    .Fail(Error<Area>.ExistedName, HttpStatusCode.Conflict);
            }

            _mapper.Map(command.Body, area);
            area.SetBranch(branch.Id);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<AreaResponse>(area);
            return Result<AreaResponse>
                .Succeed(response, Success<Area>.Updated);
        }

        public async Task<Result> DeleteAsync(
            ISpecification<Area> specification,
            CancellationToken cancellationToken = default)
        {
            var area = await _areaRepository.FindAsync(specification, cancellationToken);
            if (area is null)
            {
                return Result
                    .Fail(Error<Area>.NotFound, HttpStatusCode.NotFound);
            }

            if (area.IsDeleted)
            {
                return Result
                    .Fail(Error<Area>.AlreadyDeleted, HttpStatusCode.BadRequest);
            }

            area.SoftDelete();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Succeed(Success<Area>.Deleted);
        }

        public async Task<Result> RestoreAsync(
            ISpecification<Area> specification,
            CancellationToken cancellationToken = default)
        {
            var area = await _areaRepository.FindAsync(specification, cancellationToken);
            if (area is null)
            {
                return Result
                    .Fail(Error<Area>.NotFound, HttpStatusCode.NotFound);
            }

            if (!area.IsDeleted)
            {
                return Result
                    .Fail(Error<Area>.NotYetDeleted, HttpStatusCode.BadRequest);
            }

            area.Restore();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Succeed(Success<Area>.Restored);
        }
    }
}

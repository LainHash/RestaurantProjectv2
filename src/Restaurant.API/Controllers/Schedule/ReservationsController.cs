using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Schedule.Reservations.Commands.Create;
using Restaurant.Application.Features.Schedule.Reservations.Commands.UpdateByCustomer;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetAllByCustomer;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetByCode;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetById;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Schedule.Reservations;

namespace Restaurant.API.Controllers.Schedule
{
    [Route("api/schedule/[controller]")]
    [ApiController]
    public class ReservationsController(
        IMediator mediator,
        ICurrentUserService currentUserService)
        : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllReservationsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetReservationByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("me")]
        public async Task<IActionResult> GetAllForCustomer(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            CancellationToken cancellationToken = default)
        {
            var userId = _currentUserService.UserId;
            if (userId is null)
            {
                return Unauthorized();
            }
            var query = new GetAllReservationsByCustomerQuery(userId.Value, fromDate, toDate);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode(
            [FromRoute] string code,
            CancellationToken cancellationToken = default)
        {
            var query = new GetReservationByCodeQuery(code);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateReservationRequest body,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var command = new CreateReservationCommand(userId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("{id}/me")]
        public async Task<IActionResult> UpdateByCustomer(
            [FromRoute] Guid id,
            [FromBody] UpdateReservationByCustomerRequest body,
            CancellationToken cancellationToken = default)
        {
            var command = new UpdateReservationByCustomerCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

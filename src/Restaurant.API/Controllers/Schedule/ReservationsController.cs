using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetAll;
using Restaurant.Application.Features.Schedule.Reservations.Queries.GetById;

namespace Restaurant.API.Controllers.Schedule
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllReservationsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetReservationByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

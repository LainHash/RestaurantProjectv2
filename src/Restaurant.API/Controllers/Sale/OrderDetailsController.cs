using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancelled;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Preparing;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Ready;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Served;

namespace Restaurant.API.Controllers.Sale
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("{id}/preparing")]
        public async Task<IActionResult> PreparingOrder(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new PreparingOrderCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("{id}/ready")]
        public async Task<IActionResult> ReadyOrder(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new ReadyOrderCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("{id}/served")]
        public async Task<IActionResult> ServedOrder(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new ServedOrderCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("{id}/cancelled")]
        public async Task<IActionResult> CancelledOrder(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new CancelledOrderCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Cancel;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Prepare;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Ready;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Serve;

namespace Restaurant.API.Controllers.Sale
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        //[HttpPost("{id}/preparing")]
        //public async Task<IActionResult> PreparingOrder(
        //    [FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new PrepareOrderCommand(id);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[HttpPost("{id}/ready")]
        //public async Task<IActionResult> ReadyOrder(
        //    [FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new ReadyOrderCommand(id);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[HttpPost("{id}/served")]
        //public async Task<IActionResult> ServedOrder(
        //    [FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new ServeOrderCommand(id);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[HttpPost("{id}/cancelled")]
        //public async Task<IActionResult> CancelledOrder(
        //    [FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new CancelOrderCommand(id);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}
    }
}

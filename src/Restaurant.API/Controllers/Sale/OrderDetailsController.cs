using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Sale.OrderPreparations.Commands.Preparing;

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
    }
}

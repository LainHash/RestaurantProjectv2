using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Commerce.Carts.Commands.AddItem;
using Restaurant.Application.Features.Commerce.Carts.Commands.RemoveItem;
using Restaurant.Application.Features.Commerce.Carts.Commands.UpdateItemQuantity;
using Restaurant.Application.Features.Commerce.Carts.Queries.GetCart;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Commerce.CartItems;

namespace Restaurant.API.Controllers.Commerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController(
        IMediator mediator,
        ICurrentUserService currentUserService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        [HttpGet]
        public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
        {
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();

            var userId = _currentUserService.UserId;

            if (sessionId is null && userId is null)
                return BadRequest("X-Session-Id header is required.");

            var query = new GetCartQuery(userId, sessionId);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem(
            [FromBody] AddCartItemRequest body,
            CancellationToken cancellationToken)
        {
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();

            var userId = _currentUserService.UserId;

            if (sessionId is null && userId is null)
                return BadRequest("X-Session-Id header is required.");

            var command = new AddCartItemCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpDelete("items")]
        public async Task<IActionResult> RemoveItem(
            [FromBody] RemoveCartItemRequest body,
            CancellationToken cancellationToken)
        {
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();

            var userId = _currentUserService.UserId;

            if (sessionId is null && userId is null)
                return BadRequest("X-Session-Id header is required.");

            var command = new RemoveCartItemCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPut("items/quantity")]
        public async Task<IActionResult> UpdateItemQuantity(
            [FromBody] UpdateCartItemQuantityRequest body,
            CancellationToken cancellationToken)
        {
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();
            if (sessionId is null)
                return BadRequest("X-Session-Id header is required.");

            var userId = _currentUserService.UserId;

            var command = new UpdateCartItemQuantityCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

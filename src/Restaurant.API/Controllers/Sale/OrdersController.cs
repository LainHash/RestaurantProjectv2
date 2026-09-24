using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Sale.Orders.Commands.AddItems;
using Restaurant.Application.Features.Sale.Orders.Commands.Create;
using Restaurant.Application.Features.Sale.Orders.Commands.CreateFromCart;
using Restaurant.Application.Features.Sale.Orders.Queries.GetAll;
using Restaurant.Application.Features.Sale.Orders.Queries.GetById;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Sale.Orders;

namespace Restaurant.API.Controllers.Sale
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(
        IMediator mediator,
        ICurrentUserService currentUserService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllOrdersQuery query,
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
            var query = new GetOrderByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateOrderRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateOrderCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("{id}/items")]
        public async Task<IActionResult> AddItems(
            [FromRoute] Guid id,
            [FromBody] AddOrderItemsRequest body,
            CancellationToken cancellationToken)
        {
            var command = new AddOrderItemsCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("from-cart")]
        public async Task<IActionResult> CreateFromCart(
            [FromBody] CreateOrderFromCartRequest body,
            CancellationToken cancellationToken)
        {
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();
            var userId = _currentUserService.UserId;

            if (sessionId is null && userId is null)
                return BadRequest("Either authentication token or X-Session-Id header is required.");

            var command = new CreateOrderFromCartCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

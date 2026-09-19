using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.AddItem;
using Restaurant.Application.Features.Commerce.Wishlists.Commands.RemoveItem;
using Restaurant.Application.Features.Commerce.Wishlists.Queries.GetWishlist;
using Restaurant.Application.Services.Auth;
using Restaurant.Contract.DTOs.Commerce.WishlistItems;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Commerce
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController(
        IMediator mediator,
        ICurrentUserService currentUserService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        [HttpGet]
        public async Task<IActionResult> GetWishlist(CancellationToken cancellationToken)
        {
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();
            if (sessionId is null)
                return BadRequest("X-Session-Id header is required.");

            var userId = _currentUserService.UserId;

            var query = new GetWishlistQuery(userId, sessionId);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem(
            [FromBody] AddWishlistItemRequest body,
            CancellationToken cancellationToken)
        {
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();
            if (sessionId is null)
                return BadRequest("X-Session-Id header is required.");

            var userId = _currentUserService.UserId;

            var command = new AddWishlistItemCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpDelete("items")]
        public async Task<IActionResult> RemoveItem(
            [FromBody] RemoveWishlistItemRequest body,
            CancellationToken cancellationToken)
        {
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();
            if (sessionId is null)
                return BadRequest("X-Session-Id header is required.");

            var userId = _currentUserService.UserId;

            var command = new RemoveWishlistItemCommand(userId, sessionId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

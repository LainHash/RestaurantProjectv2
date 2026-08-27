using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Guest.Wallets.Queries.GetByUserId;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("user")]
        public async Task<IActionResult> GetByUser(CancellationToken cancellationToken)
        {
            Guid userId = Guid.Empty;

            if (User.Identity?.IsAuthenticated == true)
            {
                Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value, out userId);
            }

            var query = new GetWalletByUserIdQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

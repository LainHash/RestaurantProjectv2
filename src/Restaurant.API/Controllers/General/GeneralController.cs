using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.General.Homes.Queries.Get;

namespace Restaurant.API.Controllers.General
{
    [Route("api")]
    [ApiController]
    public class GeneralController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("Home")]
        public async Task<IActionResult> GetHome([FromQuery] GetHomeQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

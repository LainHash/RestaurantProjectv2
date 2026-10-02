using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.General.Homes.Queries.Get;
using Restaurant.Application.Services.Auth;

namespace Restaurant.API.Controllers.General
{
    [Route("api")]
    [ApiController]
    public class GeneralController(
        IMediator mediator,
        ICurrentUserService currentUserService,
        IEnumerable<EndpointDataSource> endpointSources) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        [HttpGet("Home")]
        public async Task<IActionResult> GetHome([FromQuery] int productLimit = 10,
            CancellationToken cancellationToken = default)
        {
            var sessionId = Request.Headers["X-Session-Id"].FirstOrDefault();

            var userId = _currentUserService.UserId;

            if (sessionId is null && userId is null)
                return BadRequest("X-Session-Id header is required.");

            var query = new GetHomeQuery(userId, sessionId, productLimit);

            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("routes")]
        public IActionResult GetRoutes()
        {
            var endpoints = endpointSources
                .SelectMany(es => es.Endpoints);

            var routes = endpoints
                .OfType<RouteEndpoint>()
                .Select(e => new
                {
                    Method = e.Metadata
                        .OfType<HttpMethodMetadata>()
                        .FirstOrDefault()
                        ?.HttpMethods
                        .FirstOrDefault(),

                    Route = e.RoutePattern.RawText
                });

            return Ok(routes);
        }

        [HttpGet]
        public IActionResult GetRoot()
        {
            return Ok(new
            {
                success = true,
                message = "API is running",
                version = "1.0.0"
            });
        }
    }
}

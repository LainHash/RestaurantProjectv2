using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Territory.Branches.Queries.GetAll;
using Restaurant.Application.Features.Territory.Branches.Queries.GetById;

namespace Restaurant.API.Controllers.Territory
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "SuperAdmin,Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllBranchesQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Manager")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetBranchByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

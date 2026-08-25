using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Personnel.Positions.Commands.Create;
using Restaurant.Application.Features.Personnel.Positions.Commands.Delete;
using Restaurant.Application.Features.Personnel.Positions.Commands.Restore;
using Restaurant.Application.Features.Personnel.Positions.Commands.Update;
using Restaurant.Application.Features.Personnel.Positions.Queries.GetAll;
using Restaurant.Application.Features.Personnel.Positions.Queries.GetById;
using Restaurant.Application.Features.Personnel.Positions.Queries.GetByName;
using Restaurant.Contract.DTOs.Personnel.Positions;

namespace Restaurant.API.Controllers.Personnel
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllPositionsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetPositionByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName(
            [FromRoute] string name,
            CancellationToken cancellationToken)
        {
            var query = new GetPositionByNameQuery(name);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreatePositionRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreatePositionCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdatePositionRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdatePositionCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeletePositionCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new RestorePositionCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Territory.Areas.Commands.Create;
using Restaurant.Application.Features.Territory.Areas.Commands.Delete;
using Restaurant.Application.Features.Territory.Areas.Commands.Restore;
using Restaurant.Application.Features.Territory.Areas.Commands.Update;
using Restaurant.Application.Features.Territory.Areas.Queries.GetAll;
using Restaurant.Application.Features.Territory.Areas.Queries.GetById;
using Restaurant.Contract.DTOs.Territory.Areas;

namespace Restaurant.API.Controllers.Territory
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        //[HttpGet]
        //public async Task<IActionResult> GetAll(
        //    [FromQuery] GetAllAreasQuery query,
        //    CancellationToken cancellationToken)
        //{
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(
        //    [FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var query = new GetAreaByIdQuery(id);
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[Authorize(Roles = "SuperAdmin,Admin,Manager")]
        //[HttpPost]
        //public async Task<IActionResult> Create(
        //    [FromBody] CreateAreaRequest body,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new CreateAreaCommand(body);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[Authorize(Roles = "SuperAdmin,Admin,Manager")]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(
        //    [FromRoute] Guid id,
        //    [FromBody] UpdateAreaRequest body,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new UpdateAreaCommand(id, body);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[Authorize(Roles = "SuperAdmin,Admin,Manager")]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(
        //    [FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new DeleteAreaCommand(id);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[Authorize(Roles = "SuperAdmin,Admin,Manager")]
        //[HttpPatch("{id}/restore")]
        //public async Task<IActionResult> Restore(
        //    [FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new RestoreAreaCommand(id);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}
    }
}

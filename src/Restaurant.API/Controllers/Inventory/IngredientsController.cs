using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Inventory.Ingredients.Commands.Create;
using Restaurant.Application.Features.Inventory.Ingredients.Commands.Delete;
using Restaurant.Application.Features.Inventory.Ingredients.Commands.Restore;
using Restaurant.Application.Features.Inventory.Ingredients.Commands.Update;
using Restaurant.Application.Features.Inventory.Ingredients.Queries.GetAll;
using Restaurant.Application.Features.Inventory.Ingredients.Queries.GetById;
using Restaurant.Application.Features.Inventory.IngredientStocks.Commands.UpdateQuantity;
using Restaurant.Contract.DTOs.Catalog.Ingredients;
using Restaurant.Contract.DTOs.Inventory.IngredientStocks;

namespace Restaurant.API.Controllers.Inventory
{
    [Route("api/inventory/[controller]")]
    [ApiController]
    public class IngredientsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "SuperAdmin,Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllIngredientsQuery query,
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
            var query = new GetIngredientByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateIngredientRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateIngredientCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateIngredientRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateIngredientCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteIngredientCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new RestoreIngredientCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Manager")]
        [HttpPatch("{ingredientId}/branch/{branchId}/update-quantity")]
        public async Task<IActionResult> UpdateQuantity(
            [FromRoute] Guid ingredientId,
            [FromRoute] Guid branchId,
            [FromBody] UpdateIngredientStockQuantityRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateIngredientStockQuantityCommand(ingredientId, branchId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

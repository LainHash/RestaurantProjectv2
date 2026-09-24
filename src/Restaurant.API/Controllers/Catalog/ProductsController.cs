using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Catalog.Products.Commands.Create;
using Restaurant.Application.Features.Catalog.Products.Commands.Delete;
using Restaurant.Application.Features.Catalog.Products.Commands.Restore;
using Restaurant.Application.Features.Catalog.Products.Commands.Update;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAllPopular;
using Restaurant.Application.Features.Catalog.Products.Queries.GetById;
using Restaurant.Application.Features.Inventory.ProductStocks.Commands.UpdateQuantity;
using Restaurant.Application.Features.Storage.Images.Commands.Upload;
using Restaurant.Contract.DTOs.Catalog.Products;
using Restaurant.Contract.DTOs.Inventory.ProductStocks;
using Restaurant.Contract.DTOs.Storage.Images;

namespace Restaurant.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllProductsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpGet("popular")]
        public async Task<IActionResult> GetAllPopular(
            [FromQuery] GetAllPopularProductsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateProductRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateProductRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand(id, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new RestoreProductCommand(id);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin,Manager,InventoryManager")]
        [HttpPatch("{productId}/branch/{branchId}/update-quantity")]
        public async Task<IActionResult> UpdateQuantity(
            [FromRoute] Guid productId,
            [FromRoute] Guid branchId,
            [FromBody] UpdateProductStockQuantityRequest body,
            CancellationToken cancellationToken)
        {
            var command = new UpdateProductStockQuantityCommand(productId, branchId, body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("{id}/images")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage(
            [FromRoute] Guid id,
            IFormFile file,
            [FromForm] UploadImageRequest metadata,
            CancellationToken cancellationToken)
        {
            if (file is null || file.Length == 0)
                return BadRequest("File ảnh không được để trống.");

            await using var stream = file.OpenReadStream();

            var command = new UploadProductImageCommand(id, stream, file.FileName, metadata);

            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

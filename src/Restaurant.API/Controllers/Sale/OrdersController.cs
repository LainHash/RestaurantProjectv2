using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Sale.Orders.Commands.Create;
using Restaurant.Application.Features.Sale.Orders.Queries.GetAll;
using Restaurant.Application.Features.Sale.Orders.Queries.GetById;
using Restaurant.Contract.DTOs.Sale.Orders;

namespace Restaurant.API.Controllers.Sale
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        //[HttpGet]
        //public async Task<IActionResult> GetAll(
        //    [FromQuery] GetAllOrdersQuery query,
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
        //    var query = new GetOrderByIdQuery(id);
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(
        //    [FromBody] CreateOrderRequest body,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new CreateOrderCommand(body);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}
    }
}

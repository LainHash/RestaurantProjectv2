using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Billing.Invoices.Commands.Checkout;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetAll;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetById;
using Restaurant.Contract.DTOs.Billing.Invoices;

namespace Restaurant.API.Controllers.Billing
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllInvoicesQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetInvoiceByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(
            [FromBody] CheckoutOrderRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CheckoutOrderCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

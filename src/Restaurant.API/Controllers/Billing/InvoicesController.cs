using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetAll;
using Restaurant.Application.Features.Billing.Invoices.Queries.GetById;

namespace Restaurant.API.Controllers.Billing
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        //[HttpGet]
        //public async Task<IActionResult> GetAll(
        //    [FromQuery] GetAllInvoicesQuery query,
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
        //    var query = new GetInvoiceByIdQuery(id);
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return this.ToActionResult(result);
        //}
    }
}

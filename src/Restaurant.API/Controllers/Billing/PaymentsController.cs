using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Billing.Payments.Commands.CreateUrl;
using Restaurant.Application.Features.Billing.Payments.Commands.HandleIpn;
using Restaurant.Application.Features.Billing.Payments.Queries.ProcessReturn;
using Restaurant.Contract.DTOs.Billing.Payments;

namespace Restaurant.API.Controllers.Billing
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("vnpay/create-url")]
        public async Task<IActionResult> CreateVnPayUrl(
            [FromBody] CreateVnPayPaymentUrlRequest body,
            CancellationToken cancellationToken)
        {
            var clientIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                ?? "127.0.0.1";

            var command = new CreateVnPayPaymentUrlCommand(body, clientIp);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpGet("vnpay/ipn")]
        public async Task<IActionResult> VnPayIpn(CancellationToken cancellationToken)
        {
            var queryParams = HttpContext.Request.Query
                .ToDictionary(k => k.Key, v => v.Value.ToString());

            var command = new HandleVnPayIpnCommand(queryParams);
            var response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("vnpay/return")]
        public async Task<IActionResult> VnPayReturn(CancellationToken cancellationToken)
        {
            var queryParams = HttpContext.Request.Query
                .ToDictionary(k => k.Key, v => v.Value.ToString());

            var query = new ProcessVnPayReturnQuery(queryParams);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

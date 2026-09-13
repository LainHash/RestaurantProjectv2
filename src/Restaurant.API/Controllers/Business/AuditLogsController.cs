using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Business.AuditLogs.Queries.GetAll;

namespace Restaurant.API.Controllers.Business
{
    [Route("api/audit-logs")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AuditLogsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllAuditLogsQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

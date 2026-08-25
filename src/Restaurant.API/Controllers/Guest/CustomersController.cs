using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Guest.Customers.Queries.GetAll;
using Restaurant.Application.Features.Guest.Customers.Queries.GetById;
using Restaurant.Application.Features.Guest.Customers.Queries.GetByUserId;
using Restaurant.Application.Features.Storage.Images.Commands.UpdateAvatar;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.API.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllCustomersQuery query,
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
            var query = new GetCustomerByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }


        [HttpGet("user")]
        public async Task<IActionResult> GetOne(CancellationToken cancellationToken)
        {
            Guid userId = Guid.Empty;

            if (User.Identity?.IsAuthenticated == true)
            {
                Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value, out userId);
            }

            var query = new GetCustomerByUserIdQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [HttpPost("user/images")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAvatar(
            IFormFile file,
            CancellationToken cancellationToken)
        {
            Guid userId = Guid.Empty;

            if (User.Identity?.IsAuthenticated == true)
            {
                Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value, out userId);
            }

            await using var stream = file.OpenReadStream();

            var command = new UploadAvatarCommand(userId, stream, file.FileName);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

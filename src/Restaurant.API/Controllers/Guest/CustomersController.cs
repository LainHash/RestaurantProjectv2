using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.DTOs.Pricing.Discounts;
using Restaurant.Application.Features.Guest.Customers.Queries.GetAll;
using Restaurant.Application.Features.Guest.Customers.Queries.GetById;
using Restaurant.Application.Features.Pricing.Discounts.Commands.Claim;
using Restaurant.Application.Features.Storage.Images.Commands.UpdateAvatar;
using Restaurant.Application.Services.Auth;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController(
        IMediator mediator,
        ICurrentUserService currentUserService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        //[HttpGet]
        //public async Task<IActionResult> GetAll(
        //    [FromQuery] GetAllCustomersQuery query,
        //    CancellationToken cancellationToken)
        //{
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOne(
        //    [FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var query = new GetCustomerByIdQuery(id);
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        [HttpPost("user/images")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAvatar(
            IFormFile file,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if(userId is null)
            {
                return Unauthorized();
            }

            await using var stream = file.OpenReadStream();

            var command = new UploadAvatarCommand(userId.Value, stream, file.FileName);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        //[HttpPost("user/discounts/claim")]
        //public async Task<IActionResult> ClaimDiscount(
        //    [FromBody] ClaimDiscountRequest body,
        //    CancellationToken cancellationToken)
        //{
        //    var userId = _currentUserService.PublicId;
        //    if (userId is null)
        //    {
        //        return Unauthorized();
        //    }

        //    var command = new ClaimDiscountCommand(userId.Value, body);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.DTOs.Identity.Users;
using Restaurant.Application.Features.Identity.PersonalProfiles.Commands.CompleteProfile;
using Restaurant.Application.Features.Identity.PersonalProfiles.Commands.Update;
using Restaurant.Application.Features.Identity.Users.Commands.CreateForEmployee;
using Restaurant.Application.Features.Identity.Users.Queries.GetAll;
using Restaurant.Application.Features.Identity.Users.Queries.GetById;
using Restaurant.Contract.DTOs.Identity.PersonalProfiles;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllUsersQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);
            return this.ToActionResult(result);
        }

        [AllowAnonymous]
        [HttpPost("complete-profile")]
        public async Task<IActionResult> CompleteProfile(
            [FromBody] CompleteProfileRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CompleteProfileCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }

        [Authorize]
        [HttpPost("create-employee-accounts")]
        public async Task<IActionResult> CreateForEmployee(
            [FromBody] CreateUsersForEmployeeRequest body,
            CancellationToken cancellationToken)
        {
            var command = new CreateUsersForEmployeeCommand(body);
            var result = await _mediator.Send(command, cancellationToken);
            return this.ToActionResult(result);
        }
    }
}

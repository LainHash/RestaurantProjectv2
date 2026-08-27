using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.Features.Guest.Wallets.Queries.GetByUserId;
using System.Security.Claims;

namespace Restaurant.API.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

    }
}

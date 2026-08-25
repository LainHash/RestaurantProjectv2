using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalProfileController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
    }
}

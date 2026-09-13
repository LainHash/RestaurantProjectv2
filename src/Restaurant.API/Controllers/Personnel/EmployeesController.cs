using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Extensions;
using Restaurant.Application.DTOs.Identity.PersonalProfiles;
using Restaurant.Application.DTOs.Personnel.Employees;
using Restaurant.Application.Features.Personnel.Employees.Commands.Create;
using Restaurant.Application.Features.Personnel.Employees.Queries.GetAll;
using Restaurant.Application.Features.Personnel.Employees.Queries.GetById;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Restaurant.API.Controllers.Personnel
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        //[HttpGet]
        //public async Task<IActionResult> GetAll(
        //    [FromQuery] GetAllEmployeesQuery query,
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
        //    var query = new GetEmployeeByIdQuery(id);
        //    var result = await _mediator.Send(query, cancellationToken);
        //    return this.ToActionResult(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(
        //    [FromBody] CreateEmployeeRequest body,
        //    CancellationToken cancellationToken)
        //{
        //    var command = new CreateEmployeeCommand(body);
        //    var result = await _mediator.Send(command, cancellationToken);
        //    return this.ToActionResult(result);
        //}
    }
}

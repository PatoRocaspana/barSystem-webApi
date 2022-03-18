using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Handlers.Commands;
using BarSystem.WebApi.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all possible employees.
        /// </summary>
        /// <returns>The list of employees</returns>
        /// <response code="200">Returns the list of employees</response>
        /// <response code="404">Employees List not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EmployeeDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllEmployeesQuery();
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Get employee by id.
        /// </summary>
        /// <param name="id">Id of the employee</param>
        /// <returns>The requested employee</returns>
        /// <response code="200">Returns the employee</response>
        /// <response code="404">Employee was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetEmployeeByIdQuery(id);
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Create a new Employee.
        /// </summary>
        /// <param name="employeeDto">The new employee</param>
        /// <returns>The employee created</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Employee
        ///     {
        ///         "firstName": "Johny",
        ///         "lastName": "Bravo",
        ///         "dni": "33888575",
        ///         "role": 1
        ///     }
        /// </remarks>
        /// <response code="201">Returns the employee created</response>
        /// <response code="400">Invalid Employee</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employeeDto)
        {
            var command = new CreateEmployeeCommand(employeeDto);
            var response = await _mediator.Send(command);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        /// <summary>
        /// Update an Employee.
        /// </summary>
        /// <param name="employeeDto">New employee data</param>
        /// <param name="id">Employee id to update</param>
        /// <returns>The employee updated</returns>
        /// <response code="200">Returns the employee updated.</response>
        /// <response code="400">Invalid Employee</response>
        /// <response code="404">Employee not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] EmployeeDto employeeDto, int id)
        {
            if (employeeDto.Id != id)
                return BadRequest();

            var query = new UpdateEmployeeCommand(employeeDto, id);
            var response = await _mediator.Send(query);

            return response == false ? NotFound() : NoContent();
        }

        /// <summary>
        /// Delete an Employee.
        /// </summary>
        /// <param name="id">Employee id to delete</param>
        /// <response code="204">Employee deleted succesfully</response>
        /// <response code="404">Employee not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteEmployeeCommand(id);
            var response = await _mediator.Send(command);

            if (!response)
                return NotFound();

            return NoContent();
        }
    }
}

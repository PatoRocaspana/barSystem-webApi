using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using Microsoft.AspNetCore.Mvc;

namespace BarSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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
        public async Task<IActionResult> GetAsync()
        {
            var employeeEntityList = await _employeeRepository.GetAllAsync();

            if (employeeEntityList is null)
                return NotFound();

            var employeeDtoList = employeeEntityList.Select(employeeEntity => new EmployeeDto(employeeEntity))
                                            .ToList();

            return Ok(employeeDtoList);
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
        public async Task<IActionResult> GetAsync(int id)
        {
            var employeeEntity = await _employeeRepository.GetAsync(id);

            if (employeeEntity == null)
                return NotFound();

            var employeeDto = new EmployeeDto(employeeEntity);

            return Ok(employeeDto);
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
        ///         "firstname": "Johny",
        ///         "lastname": "Bravo",
        ///         "dni": 33888575,
        ///         "role": 1
        ///     }
        /// </remarks>
        /// <response code="201">Returns the employee created</response>
        /// <response code="400">Invalid Employee</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] EmployeeDto employeeDto)
        {
            var employeeEntity = employeeDto.ToEmployeeEntity(employeeDto);

            var employeeCreated = await _employeeRepository.CreateAsync(employeeEntity);

            if (employeeCreated is null)
                return BadRequest();

            var employeeDtoCreated = new EmployeeDto(employeeCreated);

            return Ok(employeeDtoCreated);  
        }

        /// <summary>
        /// Update an Employee.
        /// </summary>
        /// <param name="employeeDto">New employee data</param>
        /// <param name="id">Employee id to update</param>
        /// <returns>The employee updated</returns>
        /// <response code="200">Returns the employee updated.</response>
        /// <response code="404">Employee not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromBody] EmployeeDto employeeDto, int id)
        {
            var dniExist = await _employeeRepository.EntityExistsAsync(id);

            if (!dniExist || employeeDto.Id != id)
                return NotFound();

            var employeeEntity = employeeDto.ToEmployeeEntity(employeeDto);

            var employeeUpdated = await _employeeRepository.UpdateAsync(employeeEntity, id);

            if (employeeUpdated is null)
                return NotFound();

            var employeeDtoUpdated = new EmployeeDto(employeeUpdated);

            return Ok(employeeDtoUpdated);
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var employeeExists = await _employeeRepository.EntityExistsAsync(id);

            if (!employeeExists)
                return NotFound();

            await _employeeRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

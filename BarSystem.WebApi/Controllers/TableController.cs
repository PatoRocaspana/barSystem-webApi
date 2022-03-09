using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Handlers.Commands;
using BarSystem.WebApi.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TableController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all possible tables.
        /// </summary>
        /// <returns>The list of tables</returns>
        /// <response code="200">Returns the list of tables</response>
        /// <response code="404">Tables List not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TableDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllTablesQuery();
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Get table by id.
        /// </summary>
        /// <param name="id">Id of the table</param>
        /// <returns>The requested table</returns>
        /// <response code="200">Returns the table</response>
        /// <response code="404">Table was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetTableByIdQuery(id);
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Create a new Table.
        /// </summary>
        /// <param name="tableDto">The new table</param>
        /// <returns>The table created</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Table
        ///     {
        ///         "CHANGE THIS": "CHANGE THIS",
        ///         "CHANGE THIS": "CHANGE THIS",
        ///         "CHANGE THIS": "CHANGE THIS",
        ///         "CHANGE THIS": CHANGE THIS
        ///     }
        /// </remarks>
        /// <response code="201">Returns the table created</response>
        /// <response code="400">Invalid Table</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TableDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] TableDto tableDto)
        {
            var command = new CreateTableCommand(tableDto);
            var response = await _mediator.Send(command);

            if (response == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        /// <summary>
        /// Update a Table.
        /// </summary>
        /// <param name="tableDto">New table data</param>
        /// <param name="id">Table id to update</param>
        /// <returns>The table updated</returns>
        /// <response code="200">Returns the table updated.</response>
        /// <response code="400">Invalid Table</response>
        /// <response code="404">Table not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TableDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] TableDto tableDto, int id)
        {
            if (tableDto.Id != id)
                return BadRequest();

            var query = new UpdateTableCommand(tableDto, id);
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Delete a Table.
        /// </summary>
        /// <param name="id">Table id to delete</param>
        /// <response code="204">Table deleted succesfully</response>
        /// <response code="404">Table not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteTableCommand(id);
            var response = await _mediator.Send(command);

            if (!response)
                return NotFound();

            return NoContent();
        }
    }
}

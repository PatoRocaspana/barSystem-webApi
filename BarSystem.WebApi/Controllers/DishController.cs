using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Handlers.Commands;
using BarSystem.WebApi.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //user: jane@example.com
        //pass: jane@example.coM

        /// <summary>
        /// Get all possible dishes.
        /// </summary>
        /// <returns>The list of dishes</returns>
        /// <response code="200">Returns the list of dishes</response>
        /// <response code="404">Dishes List not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DishDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllDishesQuery();
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Get dish by id.
        /// </summary>
        /// <param name="id">Id of the dish</param>
        /// <returns>The requested dish</returns>
        /// <response code="200">Returns the dish</response>
        /// <response code="404">Dish was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetDishByIdQuery(id);
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Create a new Dish.
        /// </summary>
        /// <param name="dishDto">The new dish</param>
        /// <returns>The dish created</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Dish
        ///     {
        ///         "name": "Crazy Steak",
        ///         "description": "Steak with Mushroom Sauce",
        ///         "price": 1390,
        ///         "stock": 23,
        ///         "category": 0,
        ///         "estimatedTime": "00:35:00",
        ///         "isReady": false
        ///     }
        /// </remarks>
        /// <response code="200">Returns the dish created</response>
        /// <response code="400">Invalid Dish</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Policy = "CreateAccess")]
        public async Task<IActionResult> Post([FromBody] DishDto dishDto)
        {
            var command = new CreateDishCommand(dishDto);
            var response = await _mediator.Send(command);

            if (response == null)
                return BadRequest();

            return Ok(response);
        }

        /// <summary>
        /// Update a Dish.
        /// </summary>
        /// <param name="dishDto">New dish data</param>
        /// <param name="id">Dish id to update</param>
        /// <returns>The dish updated</returns>
        /// <response code="200">Returns the dish updated.</response>
        /// <response code="400">Invalid Dish</response>
        /// <response code="404">Dish not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "UpdateAccess")]
        public async Task<IActionResult> Put([FromBody] DishDto dishDto, int id)
        {
            if (dishDto.Id != id)
                return BadRequest();

            var query = new UpdateDishCommand(dishDto, id);
            var response = await _mediator.Send(query);

            return response == false ? NotFound() : NoContent();
        }

        /// <summary>
        /// Delete a Dish.
        /// </summary>
        /// <param name="id">Dish id to delete</param>
        /// <response code="204">Dish deleted succesfully</response>
        /// <response code="404">Dish not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        [Authorize(Policy = "DeleteAccess")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteDishCommand(id);
            var response = await _mediator.Send(command);

            if (!response)
                return NotFound();

            return NoContent();
        }
    }
}

using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Handlers.Commands;
using BarSystem.WebApi.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BarSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DrinkController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all possible drinks.
        /// </summary>
        /// <returns>The list of drinks</returns>
        /// <response code="200">Returns the list of drinks</response>
        /// <response code="404">Drinks List not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DrinkDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllDrinksQuery();
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Get drink by id.
        /// </summary>
        /// <param name="id">Id of the drink</param>
        /// <returns>The requested drink</returns>
        /// <response code="200">Returns the drink</response>
        /// <response code="404">Drink was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DrinkDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetDrinkByIdQuery(id);
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Create a new Drink.
        /// </summary>
        /// <param name="drinkDto">The new drink</param>
        /// <returns>The drink created</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Drink
        ///     {
        ///         "name": "GinTonic",
        ///         "description": "Gin with Tonic Water",
        ///         "price": 575,
        ///         "stock": 64,
        ///         "category": 4
        ///     }
        /// </remarks>
        /// <response code="200">Returns the drink created</response>
        /// <response code="400">Invalid Drink</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DrinkDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] DrinkDto drinkDto)
        {
            var command = new CreateDrinkCommand(drinkDto);
            var response = await _mediator.Send(command);

            if (response == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
        }

        /// <summary>
        /// Update a Drink.
        /// </summary>
        /// <param name="drinkDto">New drink data</param>
        /// <param name="id">Drink id to update</param>
        /// <returns>The drink updated</returns>
        /// <response code="200">Returns the drink updated.</response>
        /// <response code="400">Invalid Drink</response>
        /// <response code="404">Drink not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DrinkDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] DrinkDto drinkDto, int id)
        {
            if (drinkDto.Id != id)
                return BadRequest();

            var query = new UpdateDrinkCommand(drinkDto, id);
            var response = await _mediator.Send(query);

            return response == null ? NotFound() : Ok(response);
        }

        /// <summary>
        /// Delete a Drink.
        /// </summary>
        /// <param name="id">Drink id to delete</param>
        /// <response code="204">Drink deleted succesfully</response>
        /// <response code="404">Drink not found</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteDrinkCommand(id);
            var response = await _mediator.Send(command);

            if (!response)
                return NotFound();

            return NoContent();
        }
    }
}

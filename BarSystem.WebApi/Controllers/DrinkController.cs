using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using Microsoft.AspNetCore.Mvc;

namespace BarSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : ControllerBase
    {
        private readonly IDrinkRepository _drinkRepository;

        public DrinkController(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
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
        public async Task<IActionResult> GetAsync()
        {
            var drinkEntityList = await _drinkRepository.GetAllAsync();

            if (drinkEntityList is null)
                return NotFound();

            var drinkDtoList = drinkEntityList.Select(drinkEntity => new DrinkDto(drinkEntity))
                                            .ToList();

            return Ok(drinkDtoList);
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
        public async Task<IActionResult> GetAsync(int id)
        {
            var drinkEntity = await _drinkRepository.GetAsync(id);

            if (drinkEntity == null)
                return NotFound();

            var drinkDto = new DrinkDto(drinkEntity);

            return Ok(drinkDto);
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
        public async Task<IActionResult> PostAsync([FromBody] DrinkDto drinkDto)
        {
            var drinkEntity = drinkDto.ToDrinkEntity(drinkDto);

            var drinkCreated = await _drinkRepository.CreateAsync(drinkEntity);

            if (drinkCreated is null)
                return BadRequest();

            var drinkDtoCreated = new DrinkDto(drinkCreated);

            return Ok(drinkDtoCreated);
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
        public async Task<IActionResult> PutAsync([FromBody] DrinkDto drinkDto, int id)
        {
            var dniExist = await _drinkRepository.EntityExistsAsync(id);

            if (!dniExist || drinkDto.Id != id)
                return NotFound();

            var drinkEntity = drinkDto.ToDrinkEntity(drinkDto);

            var drinkUpdated = await _drinkRepository.UpdateAsync(drinkEntity, id);

            if (drinkUpdated is null)
                return NotFound();

            var drinkDtoUpdated = new DrinkDto(drinkUpdated);

            return Ok(drinkDtoUpdated);
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var drinkExists = await _drinkRepository.EntityExistsAsync(id);

            if (!drinkExists)
                return NotFound();

            await _drinkRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

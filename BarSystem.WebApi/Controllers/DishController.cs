using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using Microsoft.AspNetCore.Mvc;

namespace BarSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishRepository _dishRepository;

        public DishController(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        /// <summary>
        /// Get all possible dishes.
        /// </summary>
        /// <returns>The list of dishes</returns>
        /// <response code="200">Returns the list of dishes</response>
        /// <response code="404">Dishes List not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DishDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync()
        {
            var dishEntityList = await _dishRepository.GetAllAsync();

            if (dishEntityList is null)
                return NotFound();

            var dishDtoList = dishEntityList.Select(dishEntity => new DishDto(dishEntity))
                                            .ToList();

            return Ok(dishDtoList);
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
        public async Task<IActionResult> GetAsync(int id)
        {
            var dishEntity = await _dishRepository.GetAsync(id);

            if (dishEntity == null)
                return NotFound();

            var dishDto = new DishDto(dishEntity);

            return Ok(dishDto);
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DishDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(DishDto dishDto)
        {
            var dishEntity = dishDto.ToDishEntity(dishDto);

            var dishCreated = await _dishRepository.CreateAsync(dishEntity);

            if (dishCreated is null)
                return BadRequest();

            var dishDtoCreated = new DishDto(dishCreated);

            return Ok(dishDtoCreated);
        }

        /// <summary>
        /// Update a Dish.
        /// </summary>
        /// <param name="dishDto">New dish data</param>
        /// <param name="id">Dish id to update</param>
        /// <returns>The dish updated</returns>
        /// <response code="200">Returns the dish updated.</response>
        /// <response code="404">Dish not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DishDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromBody] DishDto dishDto, int id)
        {
            var dniExist = await _dishRepository.EntityExistsAsync(id);

            if (!dniExist || dishDto.Id != id)
                return NotFound();

            var dishEntity = dishDto.ToDishEntity(dishDto);

            var dishUpdated = await _dishRepository.UpdateAsync(dishEntity, id);

            if (dishUpdated is null)
                return NotFound();

            var dishDtoUpdated = new DishDto(dishUpdated);

            return Ok(dishDtoUpdated);
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
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dishExists = await _dishRepository.EntityExistsAsync(id);

            if (!dishExists)
                return NotFound();

            await _dishRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}

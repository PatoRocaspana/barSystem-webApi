using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
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

        // GET: api/Dish
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

        // GET: api/Dish/5
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

        // POST: api/Dish
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

        //// PUT: api/Dish/5
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

        //// DELETE: api/Dish/5
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

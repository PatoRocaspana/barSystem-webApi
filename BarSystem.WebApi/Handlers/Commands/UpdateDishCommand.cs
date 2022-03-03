using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateDishCommand(DishDto DishDto, int id) : IRequest<DishDto> { }

    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, DishDto>
    {
        private readonly IDishRepository _dishRepository;

        public UpdateDishCommandHandler(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        //public async Task<DishDto> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        //{
        //    if (dishDto.Id != id)
        //        return BadRequest();

        //    var dniExist = await _dishRepository.EntityExistsAsync(id);

        //    if (!dniExist)
        //        return NotFound();

        //    var dishEntity = dishDto.ToDishEntity(dishDto);

        //    var dishUpdated = await _dishRepository.UpdateAsync(dishEntity, id);

        //    if (dishUpdated is null)
        //        return NotFound();

        //    var dishDtoUpdated = new DishDto(dishUpdated);

        //    return Ok(dishDtoUpdated);
        //}
    }
}

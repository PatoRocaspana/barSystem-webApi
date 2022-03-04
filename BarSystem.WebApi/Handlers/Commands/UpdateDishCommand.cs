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

        public async Task<DishDto> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var dniExist = await _dishRepository.EntityExistsAsync(request.id);

            if (!dniExist)
                return null;

            var dish = request.DishDto.ToDishEntity(request.DishDto);

            var dishUpdated = await _dishRepository.UpdateAsync(dish, request.id);

            if (dishUpdated is null)
                return null;

            var dishDtoUpdated = new DishDto(dishUpdated);

            return dishDtoUpdated;
        }
    }
}

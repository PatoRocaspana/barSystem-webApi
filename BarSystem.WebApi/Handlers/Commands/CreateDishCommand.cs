using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record CreateDishCommand(DishDto DishDto) : IRequest<DishDto> { }

    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, DishDto>
    {
        private readonly IDishRepository _dishRepository;

        public CreateDishCommandHandler(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<DishDto> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = request.DishDto.ToDishEntity(request.DishDto);

            var dishCreated = await _dishRepository.CreateAsync(dish);

            if (dishCreated == null)
                return null;

            var dishDtoCreated = new DishDto(dishCreated);

            return dishDtoCreated;
        }
    }
}
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateDishCommand(DishDto DishDto, int id) : IRequest<DishDto> { }

    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, DishDto>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public UpdateDishCommandHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<DishDto> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var dniExist = await _dishRepository.EntityExistsAsync(request.id);

            if (!dniExist)
                return null;

            var dish = _mapper.Map<Dish>(request.DishDto);

            var dishUpdated = await _dishRepository.UpdateAsync(dish, request.id);

            if (dishUpdated is null)
                return null;

            var dishDtoUpdated = _mapper.Map<DishDto>(dishUpdated);

            return dishDtoUpdated;
        }
    }
}

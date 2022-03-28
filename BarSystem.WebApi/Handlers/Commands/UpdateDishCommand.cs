using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateDishCommand(DishDto DishDto, int id) : IRequest<bool> { }

    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, bool>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public UpdateDishCommandHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _dishRepository.EntityExistsAsync(request.id);

            if (!entityExist)
                return false;

            var dish = _mapper.Map<Dish>(request.DishDto);

            var dishUpdated = await _dishRepository.UpdateAsync(dish, request.id);

            return dishUpdated;
        }
    }
}

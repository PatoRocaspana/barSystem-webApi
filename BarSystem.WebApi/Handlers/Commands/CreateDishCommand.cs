using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record CreateDishCommand(DishDto DishDto) : IRequest<int?> { }

    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int?>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public CreateDishCommandHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<int?> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            var dish = _mapper.Map<Dish>(request.DishDto);

            var dishCreated = await _dishRepository.CreateAsync(dish);

            return dishCreated;
        }
    }
}

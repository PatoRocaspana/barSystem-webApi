using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record CreateTableCommand(TableDto TableDto) : IRequest<int?> { }

    public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, int?>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;

        public CreateTableCommandHandler(ITableRepository tableRepository, IDishRepository dishRepository, IDrinkRepository drinkRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _dishRepository = dishRepository;
            _drinkRepository = drinkRepository;
            _mapper = mapper;
        }

        public async Task<int?> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            var table = _mapper.Map<Table>(request.TableDto);

            foreach (var dish in request.TableDto.DishIds)
            {
                var existingDish = await _dishRepository.GetAsync(dish);
                table.Dishes.Add(existingDish);
            }

            foreach (var drink in request.TableDto.DrinksIds)
            {
                var existingDrink = await _drinkRepository.GetAsync(drink);
                table.Drinks.Add(existingDrink);
            }

            var tableCreated = await _tableRepository.CreateAsync(table);

            return tableCreated;
        }
    }
}

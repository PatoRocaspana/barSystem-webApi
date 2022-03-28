using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateTableCommand(TableDto TableDto, int id) : IRequest<bool> { }

    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, bool>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;

        public UpdateTableCommandHandler(ITableRepository tableRepository, IDishRepository dishRepository, IDrinkRepository drinkRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _dishRepository = dishRepository;
            _drinkRepository = drinkRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _tableRepository.EntityExistsAsync(request.id);

            if (!entityExist)
                return false;

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

            var tableUpdated = await _tableRepository.UpdateAsync(table, request.id);

            return tableUpdated;
        }
    }
}

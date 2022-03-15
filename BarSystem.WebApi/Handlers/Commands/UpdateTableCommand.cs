using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateTableCommand(TableDto TableDto, int id) : IRequest<TableInfoDto> { }

    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, TableInfoDto>
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

        public async Task<TableInfoDto> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var dniExist = await _tableRepository.EntityExistsAsync(request.id);

            if (!dniExist)
                return null;

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

            if (tableUpdated is null)
                return null;

            var tableDtoUpdated = _mapper.Map<TableInfoDto>(tableUpdated);

            return tableDtoUpdated;
        }
    }
}

using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record CreateTableCommand(TableDto TableDto) : IRequest<TableDto> { }

    public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, TableDto>
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

        public async Task<TableDto> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            var table = _mapper.Map<Table>(request.TableDto);

            foreach (var dish in request.TableDto.DishIds)
            {
                table.Dishes.Add(await _dishRepository.GetAsync(dish));
            }

            foreach (var drink in request.TableDto.DrinksIds)
            {
                table.Drinks.Add(await _drinkRepository.GetAsync(drink));
            }

            var tableCreated = await _tableRepository.CreateAsync(table);

            if (tableCreated == null)
                return null;

            var tableDtoCreated = _mapper.Map<TableDto>(tableCreated);

            return tableDtoCreated;
        }
    }
}

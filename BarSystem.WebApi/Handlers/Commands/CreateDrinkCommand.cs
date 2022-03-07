using AutoMapper;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record CreateDrinkCommand(DrinkDto DrinkDto) : IRequest<DrinkDto> { }

    public class CreateDrinkCommandHandler : IRequestHandler<CreateDrinkCommand, DrinkDto>
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;

        public CreateDrinkCommandHandler(IDrinkRepository drinkRepository, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _mapper = mapper;
        }

        public async Task<DrinkDto> Handle(CreateDrinkCommand request, CancellationToken cancellationToken)
        {
            var drink = _mapper.Map<Drink>(request.DrinkDto);

            var drinkCreated = await _drinkRepository.CreateAsync(drink);

            if (drinkCreated == null)
                return null;

            var drinkDtoCreated = _mapper.Map<DrinkDto>(drinkCreated);

            return drinkDtoCreated;
        }
    }
}

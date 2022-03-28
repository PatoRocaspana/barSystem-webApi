using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateDrinkCommand(DrinkDto DrinkDto, int id) : IRequest<bool> { }

    public class UpdateDrinkCommandHandler : IRequestHandler<UpdateDrinkCommand, bool>
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;

        public UpdateDrinkCommandHandler(IDrinkRepository drinkRepository, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateDrinkCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _drinkRepository.EntityExistsAsync(request.id);

            if (!entityExist)
                return false;

            var drink = _mapper.Map<Drink>(request.DrinkDto);

            var drinkUpdated = await _drinkRepository.UpdateAsync(drink, request.id);

            return drinkUpdated;
        }
    }
}

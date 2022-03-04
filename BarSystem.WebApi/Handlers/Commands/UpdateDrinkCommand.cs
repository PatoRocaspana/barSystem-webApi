using AutoMapper;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateDrinkCommand(DrinkDto DrinkDto, int id) : IRequest<DrinkDto> { }

    public class UpdateDrinkCommandHandler : IRequestHandler<UpdateDrinkCommand, DrinkDto>
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;

        public UpdateDrinkCommandHandler(IDrinkRepository drinkRepository, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _mapper = mapper;
        }

        public async Task<DrinkDto> Handle(UpdateDrinkCommand request, CancellationToken cancellationToken)
        {
            var dniExist = await _drinkRepository.EntityExistsAsync(request.id);

            if (!dniExist)
                return null;

            var drink = _mapper.Map<Drink>(request.DrinkDto);

            var drinkUpdated = await _drinkRepository.UpdateAsync(drink, request.id);

            if (drinkUpdated is null)
                return null;

            var drinkDtoUpdated = _mapper.Map<DrinkDto>(drink);

            return drinkDtoUpdated;
        }
    }
}

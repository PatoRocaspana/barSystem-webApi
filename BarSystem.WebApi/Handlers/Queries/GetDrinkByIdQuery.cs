using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetDrinkByIdQuery(int Id) : IRequest<DrinkDto> { }

    public class GetDrinkByIdQueryHandler : IRequestHandler<GetDrinkByIdQuery, DrinkDto>
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;

        public GetDrinkByIdQueryHandler(IDrinkRepository drinkRepository, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _mapper = mapper;
        }

        public async Task<DrinkDto> Handle(GetDrinkByIdQuery request, CancellationToken cancellationToken)
        {
            var drink = await _drinkRepository.GetAsync(request.Id);

            if (drink == null)
                return null;

            var drinkDto = _mapper.Map<DrinkDto>(drink);

            return drinkDto;
        }
    }
}

using AutoMapper;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetAllDrinksQuery : IRequest<List<DrinkDto>> { }

    public class GetAllDrinksQueryHandler : IRequestHandler<GetAllDrinksQuery, List<DrinkDto>>
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;

        public GetAllDrinksQueryHandler(IDrinkRepository DrinkRepository, IMapper mapper)
        {
            _drinkRepository = DrinkRepository;
            _mapper = mapper;
        }

        public async Task<List<DrinkDto>> Handle(GetAllDrinksQuery request, CancellationToken cancellationToken)
        {
            var drinks = await _drinkRepository.GetAllAsync();

            if (drinks == null)
                return null;

            var DrinkDtoList = _mapper.Map<List<DrinkDto>>(drinks);

            return DrinkDtoList;
        }
    }
}


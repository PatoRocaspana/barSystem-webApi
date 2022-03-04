using AutoMapper;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetAllDishesQuery : IRequest<List<DishDto>> { }

    public class GetAllDishesQueryHandler : IRequestHandler<GetAllDishesQuery, List<DishDto>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public GetAllDishesQueryHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<List<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _dishRepository.GetAllAsync();

            if (dishes == null)
                return null;

            var dishDtoList =  _mapper.Map<List<DishDto>>(dishes);

            return dishDtoList;
        }
    }
}

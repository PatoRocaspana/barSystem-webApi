using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetAllDishesQuery : IRequest<List<DishDto>> { }

    public class GetAllDishesQueryHandler : IRequestHandler<GetAllDishesQuery, List<DishDto>>
    {
        private readonly IDishRepository _dishRepository;

        public GetAllDishesQueryHandler(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<List<DishDto>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
        {
            var dishes = await _dishRepository.GetAllAsync();

            if (dishes == null)
                return null;

            var dishDtoList = dishes.Select(dishEntity => new DishDto(dishEntity))
                                    .ToList();

            return dishDtoList;
        }
    }
}

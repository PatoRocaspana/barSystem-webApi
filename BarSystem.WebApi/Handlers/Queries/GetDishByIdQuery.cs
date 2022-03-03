using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetDishByIdQuery(int Id) : IRequest<DishDto> { }

    public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishDto>
    {
        private readonly IDishRepository _dishRepository;

        public GetDishByIdQueryHandler(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.GetAsync(request.Id);

            if (dish == null)
                return null;

            var dishDto = new DishDto(dish);

            return dishDto;
        }
    }
}

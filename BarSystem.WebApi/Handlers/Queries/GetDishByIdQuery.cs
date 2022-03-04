using AutoMapper;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetDishByIdQuery(int Id) : IRequest<DishDto> { }

    public class GetDishByIdQueryHandler : IRequestHandler<GetDishByIdQuery, DishDto>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public GetDishByIdQueryHandler(IDishRepository dishRepository, IMapper mapper)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.GetAsync(request.Id);

            if (dish == null)
                return null;

            var dishDto = _mapper.Map<DishDto>(dish);

            return dishDto;
        }
    }
}

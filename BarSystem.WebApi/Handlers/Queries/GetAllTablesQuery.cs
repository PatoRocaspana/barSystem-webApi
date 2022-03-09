using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetAllTablesQuery : IRequest<List<TableDto>> { }

    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTablesQuery, List<TableDto>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetAllTablesQueryHandler(ITableRepository TableRepository, IMapper mapper)
        {
            _tableRepository = TableRepository;
            _mapper = mapper;
        }

        public async Task<List<TableDto>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var tables = await _tableRepository.GetAllAsync();

            if (tables == null)
                return null;

            var TableDtoList = _mapper.Map<List<TableDto>>(tables);

            return TableDtoList;
        }
    }
}

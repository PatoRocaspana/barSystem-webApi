using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetAllTablesQuery : IRequest<List<TableInfoDto>> { }

    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTablesQuery, List<TableInfoDto>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetAllTablesQueryHandler(ITableRepository TableRepository, IMapper mapper)
        {
            _tableRepository = TableRepository;
            _mapper = mapper;
        }

        public async Task<List<TableInfoDto>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var tables = await _tableRepository.GetAllAsync();

            if (tables == null)
                return null;

            var TableInfoDtoList = _mapper.Map<List<TableInfoDto>>(tables);

            return TableInfoDtoList;
        }
    }
}

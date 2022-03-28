using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetTableByIdQuery(int Id) : IRequest<TableInfoDto> { }

    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, TableInfoDto>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetTableByIdQueryHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<TableInfoDto> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetAsync(request.Id);

            if (table == null)
                return null;

            var tableInfoDto = _mapper.Map<TableInfoDto>(table);

            return tableInfoDto;
        }
    }
}

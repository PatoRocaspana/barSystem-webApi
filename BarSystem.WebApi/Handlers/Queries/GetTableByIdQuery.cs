using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetTableByIdQuery(int Id) : IRequest<TableDto> { }

    public class GetTableByIdQueryHandler : IRequestHandler<GetTableByIdQuery, TableDto>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetTableByIdQueryHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<TableDto> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetAsync(request.Id);

            if (table == null)
                return null;

            var tableDto = _mapper.Map<TableDto>(table);

            return tableDto;
        }
    }
}

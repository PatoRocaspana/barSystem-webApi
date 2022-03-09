using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record CreateTableCommand(TableDto TableDto) : IRequest<TableDto> { }

    public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, TableDto>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public CreateTableCommandHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<TableDto> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            var table = _mapper.Map<Table>(request.TableDto);

            var tableCreated = await _tableRepository.CreateAsync(table);

            if (tableCreated == null)
                return null;

            var tableDtoCreated = _mapper.Map<TableDto>(tableCreated);

            return tableDtoCreated;
        }
    }
}

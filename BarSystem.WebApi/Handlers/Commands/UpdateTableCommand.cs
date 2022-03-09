using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateTableCommand(TableDto TableDto, int id) : IRequest<TableDto> { }

    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, TableDto>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public UpdateTableCommandHandler(ITableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<TableDto> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            var dniExist = await _tableRepository.EntityExistsAsync(request.id);

            if (!dniExist)
                return null;

            var table = _mapper.Map<Table>(request.TableDto);

            var tableUpdated = await _tableRepository.UpdateAsync(table, request.id);

            if (tableUpdated is null)
                return null;

            var tableDtoUpdated = _mapper.Map<TableDto>(tableUpdated);

            return tableDtoUpdated;
        }
    }
}

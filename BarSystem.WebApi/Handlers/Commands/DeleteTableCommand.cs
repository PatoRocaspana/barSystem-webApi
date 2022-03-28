using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record DeleteTableCommand(int id) : IRequest<bool> { }

    public class DeleteTableCommandHandler : IRequestHandler<DeleteTableCommand, bool>
    {
        private readonly ITableRepository _tableRepository;

        public DeleteTableCommandHandler(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<bool> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            var tableExists = await _tableRepository.EntityExistsAsync(request.id);

            if (!tableExists)
                return false;

            await _tableRepository.DeleteAsync(request.id);
            return true;
        }
    }
}

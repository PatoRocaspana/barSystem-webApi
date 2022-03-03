using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record DeleteDishCommand(int id) : IRequest<bool> { }

    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand, bool>
    {
        private readonly IDishRepository _dishRepository;

        public DeleteDishCommandHandler(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<bool> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var dishExists = await _dishRepository.EntityExistsAsync(request.id);

            if (!dishExists)
                return false;

            await _dishRepository.DeleteAsync(request.id);
            return true;
        }
    }
}

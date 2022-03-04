using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record DeleteDrinkCommand(int id) : IRequest<bool> { }

    public class DeleteDrinkCommandHandler : IRequestHandler<DeleteDrinkCommand, bool>
    {
        private readonly IDrinkRepository _drinkRepository;

        public DeleteDrinkCommandHandler(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }

        public async Task<bool> Handle(DeleteDrinkCommand request, CancellationToken cancellationToken)
        {
            var drinkExists = await _drinkRepository.EntityExistsAsync(request.id);

            if (!drinkExists)
                return false;

            await _drinkRepository.DeleteAsync(request.id);
            return true;
        }
    }
}

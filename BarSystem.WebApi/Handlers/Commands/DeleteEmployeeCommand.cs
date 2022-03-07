using BarSystem.WebApi.Interfaces.Data;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record DeleteEmployeeCommand(int id) : IRequest<bool> { }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeExists = await _employeeRepository.EntityExistsAsync(request.id);

            if (!employeeExists)
                return false;

            await _employeeRepository.DeleteAsync(request.id);
            return true;
        }
    }
}

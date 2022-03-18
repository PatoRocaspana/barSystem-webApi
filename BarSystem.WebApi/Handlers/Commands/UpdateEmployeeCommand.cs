using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateEmployeeCommand(EmployeeDto EmployeeDto, int id) : IRequest<bool> { }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _employeeRepository.EntityExistsAsync(request.id);

            if (!entityExist)
                return false;

            var employee = _mapper.Map<Employee>(request.EmployeeDto);

            var employeeUpdated = await _employeeRepository.UpdateAsync(employee, request.id);

            return employeeUpdated;
        }
    }
}

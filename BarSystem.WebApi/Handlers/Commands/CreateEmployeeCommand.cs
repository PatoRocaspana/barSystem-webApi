using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record CreateEmployeeCommand(EmployeeDto EmployeeDto) : IRequest<EmployeeDto> { }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request.EmployeeDto);

            var employeeCreated = await _employeeRepository.CreateAsync(employee);

            if (employeeCreated == null)
                return null;

            var employeeDtoCreated = _mapper.Map<EmployeeDto>(employeeCreated);

            return employeeDtoCreated;
        }
    }
}

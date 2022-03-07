using AutoMapper;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using MediatR;

namespace BarSystem.WebApi.Handlers.Commands
{
    public record UpdateEmployeeCommand(EmployeeDto EmployeeDto, int id) : IRequest<EmployeeDto> { }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var dniExist = await _employeeRepository.EntityExistsAsync(request.id);

            if (!dniExist)
                return null;

            var employee = _mapper.Map<Employee>(request.EmployeeDto);

            var employeeUpdated = await _employeeRepository.UpdateAsync(employee, request.id);

            if (employeeUpdated is null)
                return null;

            var employeeDtoUpdated = _mapper.Map<EmployeeDto>(employeeUpdated);

            return employeeDtoUpdated;
        }
    }
}

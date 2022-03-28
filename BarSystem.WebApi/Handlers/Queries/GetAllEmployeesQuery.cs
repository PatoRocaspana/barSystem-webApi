using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using MapsterMapper;
using MediatR;

namespace BarSystem.WebApi.Handlers.Queries
{
    public record GetAllEmployeesQuery : IRequest<List<EmployeeDto>> { }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository EmployeeRepository, IMapper mapper)
        {
            _employeeRepository = EmployeeRepository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();

            if (employees == null)
                return null;

            var EmployeeDtoList = _mapper.Map<List<EmployeeDto>>(employees);

            return EmployeeDtoList;
        }
    }
}

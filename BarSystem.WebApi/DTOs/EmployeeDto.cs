using BarSystem.WebApi.DTOs.Base;
using BarSystem.WebApi.Models;
using BarSystem.WebApi.Models.Enum;

namespace BarSystem.WebApi.DTOs
{
    public class EmployeeDto : EntityDto
    {
        public EmployeeDto() { }

        public EmployeeDto(Employee employeeEntity)
        {
            Id = employeeEntity.Id;
            Dni = employeeEntity.Dni;
            FirstName = employeeEntity.FirstName;
            LastName = employeeEntity.LastName;
            Role = employeeEntity.Role;
        }

        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }

        public Employee ToEmployeeEntity(EmployeeDto employeeDto)
        {
            var employeeEntity = new Employee()
            {
                Id = employeeDto.Id,
                Dni = employeeDto.Dni,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Role = employeeDto.Role
            };

            return employeeEntity;
        }
    }
}

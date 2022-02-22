using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarSystem.WebApi.Data
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BarSystemDbContext dbContext) : base(dbContext) { }

        protected override void UpdateEntity(Employee existingEmployee, Employee newEmployee)
        {
            existingEmployee.Dni = newEmployee.Dni;
            existingEmployee.FirstName = newEmployee.FirstName;
            existingEmployee.LastName = newEmployee.LastName;
            existingEmployee.Role = newEmployee.Role;
        }
    }
}

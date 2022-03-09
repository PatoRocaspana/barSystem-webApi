using BarSystem.WebApi.DTOs.Base;
using BarSystem.WebApi.Models.Enum;

namespace BarSystem.WebApi.DTOs
{
    public class EmployeeDto : EntityDto
    {
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }
}

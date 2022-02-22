using BarSystem.WebApi.Models.Base;
using BarSystem.WebApi.Models.Enum;

namespace BarSystem.WebApi.Models
{
    public class Employee : Entity
    {
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }
}

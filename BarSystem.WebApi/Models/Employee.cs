using BarSystem.Models.Base;
using BarSystem.Models.Enum;

namespace BarSystem.Models
{
    public class Employee : Entity
    {
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
    }
}

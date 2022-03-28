using BarSystem.WebApi.Models.Base;
using BarSystem.WebApi.Models.Enum;

namespace BarSystem.WebApi.Models
{
    public class Drink : Product
    {
        public DrinkCategory Category { get; set; }

        public List<Table> Tables { get; set; } = new List<Table>();
    }
}

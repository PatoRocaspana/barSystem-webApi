using BarSystem.WebApi.Models.Base;
using BarSystem.WebApi.Models.Enum;

namespace BarSystem.WebApi.Models
{
    public class Dish : Product
    {
        public FoodCategory Category { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public bool IsReady { get; set; }

        public List<Table> Tables { get; set; } = new List<Table>();
    }
}

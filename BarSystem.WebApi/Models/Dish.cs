using BarSystem.Models.Enum;
using BarSystem.WebApi.Models.Base;

namespace BarSystem.Models
{
    public class Dish : Product
    {
        public FoodCategory Category { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public bool IsReady { get; set; }
    }
}

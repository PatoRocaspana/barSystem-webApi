using BarSystem.WebApi.DTOs.Base;
using BarSystem.WebApi.Models.Enum;

namespace BarSystem.WebApi.DTOs
{
    public class DishDto : ProductDto
    {
        public FoodCategory Category { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public bool IsReady { get; set; }
    }
}

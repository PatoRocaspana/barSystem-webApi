using BarSystem.WebApi.DTOs.Base;
using BarSystem.WebApi.Models.Enum;

namespace BarSystem.WebApi.DTOs
{
    public class DrinkDto : ProductDto
    {
        public DrinkCategory Category { get; set; }
    }
}

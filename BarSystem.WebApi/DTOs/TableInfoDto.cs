using BarSystem.WebApi.DTOs.Base;

namespace BarSystem.WebApi.DTOs
{
    public class TableInfoDto : EntityDto
    {
        public int EmployeeId { get; set; }
        public int AmountPeople { get; set; }
        public bool ExistAdult { get; set; }
        public List<ProductInfoDto> DishesInfoDto { get; set; } = new List<ProductInfoDto>();
        public List<ProductInfoDto> DrinksInfoDto { get; set; } = new List<ProductInfoDto>();
        public float TotalPrice { get; set; }
    }
}

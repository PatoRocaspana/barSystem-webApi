using BarSystem.WebApi.DTOs.Base;

namespace BarSystem.WebApi.DTOs
{
    public class TableDto : EntityDto
    {
        public EmployeeDto EmployeeDto { get; set; }
        public int AmountPeople { get; set; }
        public bool ExistAdult { get; set; }
        public List<DrinkDto> DrinksDto { get; set; }
        public List<DishDto> DishesDto { get; set; }
        public float TotalPrice { get; set; }
    }
}

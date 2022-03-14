using BarSystem.WebApi.DTOs.Base;

namespace BarSystem.WebApi.DTOs
{
    public class TableDto : EntityDto
    {
        public int EmployeeId { get; set; }
        public int AmountPeople { get; set; }
        public bool ExistAdult { get; set; }
        public List<int> DishIds { get; set; } = new List<int>();
        public List<int> DrinksIds { get; set; } = new List<int>();
        public float TotalPrice { get; set; }
    }
}

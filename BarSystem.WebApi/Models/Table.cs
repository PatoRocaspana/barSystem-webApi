using BarSystem.Models.Base;
using BarSystem.WebApi.Models;

namespace BarSystem.Models
{
    public class Table : Entity
    {
        public Employee Waiter { get; set; }
        public int AmountPeople { get; set; }
        public List<Drink> Drinks { get; set; } = new List<Drink>();
        public List<Dish> Dishes { get; set; } = new List<Dish>();
        public float TotalPrice { get; set; }
    }
}

using BarSystem.Models.Base;
using BarSystem.WebApi.Models;

namespace BarSystem.Models
{
    public class Table : Entity
    {
        public Employee Waiter { get; set; }
        public int AmountPeople { get; set; }
        public bool ExistAdult { get; set; }
        public List<Drink> Drinks { get; set; } = new List<Drink>();
        public List<Dish> Dishes { get; set; } = new List<Dish>();
        public float TotalPrice => GetTotalPrice();

        private float GetTotalPrice()
        {
            var totalPrice = Drinks.Sum(drink => drink.Price);
            totalPrice += Dishes.Sum(dish => dish.Price);
            return totalPrice;
        }
    }
}

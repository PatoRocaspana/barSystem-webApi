using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarSystem.WebApi.Data
{
    public class DrinkRepository : BaseRepository<Drink>, IDrinkRepository
    {
        public DrinkRepository(BarSystemDbContext dbContext) : base(dbContext) { }

        protected override void UpdateEntity(Drink existingDrink, Drink newDrink)
        {
            existingDrink.Name = newDrink.Name;
            existingDrink.Description = newDrink.Description;
            existingDrink.Stock = newDrink.Stock;
            existingDrink.Price = newDrink.Price;
            existingDrink.Category = newDrink.Category;
        }
    }
}

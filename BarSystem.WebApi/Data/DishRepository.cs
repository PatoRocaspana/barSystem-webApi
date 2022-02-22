using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BarSystem.WebApi.Data
{
    public class DishRepository : BaseRepository<Dish>, IDishRepository
    {
        public DishRepository(BarSystemDbContext dbContext) : base(dbContext) { }

        protected override void UpdateEntity(Dish existingDish, Dish newDish)
        {
            existingDish.Name = newDish.Name;
            existingDish.Description = newDish.Description;
            existingDish.Stock = newDish.Stock;
            existingDish.Price = newDish.Price;
            existingDish.Category = newDish.Category;
            existingDish.EstimatedTime = newDish.EstimatedTime;
            existingDish.IsReady = newDish.IsReady;
        }
    }
}

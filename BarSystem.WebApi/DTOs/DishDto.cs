using BarSystem.WebApi.DTOs.Base;
using BarSystem.WebApi.Models;
using BarSystem.WebApi.Models.Enum;

namespace BarSystem.WebApi.DTOs
{
    public class DishDto : ProductDto
    {
        public DishDto() { }

        public DishDto(Dish dishEntity)
        {
            Id = dishEntity.Id;
            Name = dishEntity.Name;
            Description = dishEntity.Description;
            Price = dishEntity.Price;
            Stock = dishEntity.Stock;
            Category = dishEntity.Category;
            EstimatedTime = dishEntity.EstimatedTime;
            IsReady = dishEntity.IsReady;
        }

        public FoodCategory Category { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public bool IsReady { get; set; }

        public Dish ToDishEntity(DishDto dishDto)
        {
            var dishEntity = new Dish()
            {
                Id = dishDto.Id,
                Name = dishDto.Name,
                Description = dishDto.Description,
                Price = dishDto.Price,
                Stock = dishDto.Stock,
                Category = dishDto.Category,
                EstimatedTime= dishDto.EstimatedTime,
                IsReady = dishDto.IsReady,
            };

            return dishEntity;
        }
    }
}

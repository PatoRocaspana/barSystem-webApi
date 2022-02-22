using BarSystem.WebApi.DTOs.Base;
using BarSystem.WebApi.Models;
using BarSystem.WebApi.Models.Enum;

namespace BarSystem.WebApi.DTOs
{
    public class DrinkDto : ProductDto
    {
        public DrinkDto() { }

        public DrinkDto(Drink drinkEntity)
        {
            Id = drinkEntity.Id;
            Name = drinkEntity.Name;
            Description = drinkEntity.Description;
            Price = drinkEntity.Price;
            Stock = drinkEntity.Stock;
            Category = drinkEntity.Category;   
        }

        public DrinkCategory Category { get; set; }

        public Drink ToDrinkEntity(DrinkDto drinkDto)
        {
            var drinkEntity = new Drink()
            {
                Id = drinkDto.Id,
                Name = drinkDto.Name,
                Description = drinkDto.Description,
                Price = drinkDto.Price,
                Stock = drinkDto.Stock,
                Category = drinkDto.Category,
            };

            return drinkEntity;
        }
    }
}

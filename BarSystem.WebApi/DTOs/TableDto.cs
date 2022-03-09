using BarSystem.WebApi.DTOs.Base;
using BarSystem.WebApi.Models;

namespace BarSystem.WebApi.DTOs
{
    public class TableDto : EntityDto
    {
        public TableDto() { }

        //public TableDto(Table tableEntity)
        //{
        //    Id = tableEntity.Id;
        //    WaiterDto = new EmployeeDto(tableEntity.Waiter);
        //    AmountPeople = tableEntity.AmountPeople;
        //    ExistAdult = tableEntity.ExistAdult;
        //    DrinksDto = ToDrinkDtoList(tableEntity.Drinks);
        //    DishesDto = ToDishDtoList(tableEntity.Dishes);
        //    TotalPrice = tableEntity.TotalPrice;
        //}

        public EmployeeDto WaiterDto { get; set; }
        public int AmountPeople { get; set; }
        public bool ExistAdult { get; set; }
        public List<DrinkDto> DrinksDto { get; set; }
        public List<DishDto> DishesDto { get; set; }
        public float TotalPrice { get; set; }

        //public Table ToTableEntity(TableDto tableDto)
        //{
        //    var tableEntity = new Table()
        //    {
        //        Id = tableDto.Id,
        //        Waiter = WaiterDto.ToEmployeeEntity(tableDto.WaiterDto),
        //        AmountPeople = tableDto.AmountPeople,
        //        ExistAdult = tableDto.ExistAdult,
        //        Drinks = ToDrinkList(tableDto.DrinksDto),
        //        Dishes = ToDishList(tableDto.DishesDto),
        //    };

        //    return tableEntity;
        //}

        private List<DrinkDto> ToDrinkDtoList(List<Drink> drinkEntityList)
        {
            var drinkDtoList = drinkEntityList.Select(drinkEntity => new DrinkDto(drinkEntity))
                                              .ToList();

            return drinkDtoList;
        }

        private List<DishDto> ToDishDtoList(List<Dish> dishEntityList)
        {
            var dishDtoList = dishEntityList.Select(dishEntity => new DishDto(dishEntity))
                                            .ToList();

            return dishDtoList;
        }

        private List<Drink> ToDrinkList(List<DrinkDto> drinkDtoList)
        {
            var drinkEntityList = drinkDtoList.Select(drinkDto => drinkDto.ToDrinkEntity(drinkDto))
                                              .ToList();

            return drinkEntityList;
        }

        private List<Dish> ToDishList(List<DishDto> dishDtoList)
        {
            var dishEntityList = dishDtoList.Select(dishDto => dishDto.ToDishEntity(dishDto))
                                            .ToList();

            return dishEntityList;
        }
    }
}

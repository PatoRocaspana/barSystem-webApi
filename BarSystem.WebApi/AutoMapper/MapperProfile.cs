using AutoMapper;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Models;

namespace BarSystem.WebApi.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DishDto, Dish>().ReverseMap();
            CreateMap<DrinkDto, Drink>().ReverseMap();
            CreateMap<EmployeeDto, Employee>().ReverseMap();
        }
    }
}

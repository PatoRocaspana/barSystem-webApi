using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Validations.Base;
using FluentValidation;

namespace BarSystem.WebApi.Validations
{
    public class DishDtoValidator : ProductDtoValidator<DishDto>
    {
        public DishDtoValidator()
        {
            RuleFor(dish => dish.EstimatedTime)
                                    .InclusiveBetween(TimeSpan.Zero, TimeSpan.FromMinutes(150))
                                    .WithMessage("{PropertyName} must be between 0 and 150 minutes");

            RuleFor(dish => dish.Category)
                                    .IsInEnum();
        }
    }
}

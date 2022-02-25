using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Validations.Base;
using FluentValidation;

namespace BarSystem.WebApi.Validations
{
    public class DrinkDtoValidator : ProductDtoValidator<DrinkDto>
    {
        public DrinkDtoValidator()
        {
            RuleFor(drink => drink.Category)
                                    .IsInEnum();
        }
    }
}

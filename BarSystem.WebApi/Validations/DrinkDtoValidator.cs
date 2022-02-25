using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Validations.Base;
using FluentValidation;

namespace BarSystem.WebApi.Validations
{
    public class DrinkDtoValidator : AbstractValidator<DrinkDto>
    {
        public DrinkDtoValidator()
        {
            Include(new ProductDtoValidator());

            RuleFor(prop => prop.Category)
                                    .IsInEnum();
        }
    }
}

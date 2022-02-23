using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Validations.Base;
using FluentValidation;

namespace BarSystem.WebApi.Validations
{
    public class DishDtoValidator : AbstractValidator<DishDto>
    {
        public DishDtoValidator()
        {
            Include(new ProductDtoValidator());

            RuleFor(prop => prop.EstimatedTime)
                                    .InclusiveBetween(TimeSpan.Zero, TimeSpan.FromMinutes(150)).WithMessage("{PropertyName} must be between 0 and 150 minutes");

            RuleFor(prop => prop.Category)
                                    .IsInEnum();
        }
    }
}

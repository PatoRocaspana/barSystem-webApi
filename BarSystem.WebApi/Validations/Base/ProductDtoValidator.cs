using BarSystem.WebApi.DTOs.Base;
using FluentValidation;

namespace BarSystem.WebApi.Validations.Base
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(prop => prop.Name)
                                    .Cascade(CascadeMode.Stop)
                                    .NotEmpty()
                                    .Length(2, 30);

            RuleFor(prop => prop.Description)
                                    .MaximumLength(200);

            RuleFor(prop => prop.Price)
                                    .GreaterThan(0);

            RuleFor(prop => prop.Stock)
                                    .GreaterThanOrEqualTo(0);
        }
    }
}

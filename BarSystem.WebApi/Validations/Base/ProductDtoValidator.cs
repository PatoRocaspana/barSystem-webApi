using BarSystem.WebApi.DTOs.Base;
using FluentValidation;

namespace BarSystem.WebApi.Validations.Base
{
    public abstract class ProductDtoValidator<T> : AbstractValidator<T> where T : ProductDto
    {
        public ProductDtoValidator()
        {
            RuleFor(product => product.Id)
                                    .GreaterThanOrEqualTo(0);

            RuleFor(product => product.Name)
                                    .Cascade(CascadeMode.Stop)
                                    .NotEmpty()
                                    .Length(2, 30);

            RuleFor(product => product.Description)
                                    .MaximumLength(200);

            RuleFor(product => product.Price)
                                    .GreaterThan(0);

            RuleFor(product => product.Stock)
                                    .GreaterThanOrEqualTo(0);
        }
    }
}

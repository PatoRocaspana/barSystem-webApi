using BarSystem.WebApi.DTOs;
using FluentValidation;

namespace BarSystem.WebApi.Validations
{
    public class TableDtoValidator : AbstractValidator<TableDto>
    {
        public TableDtoValidator()
        {
            RuleFor(prop => prop.WaiterDto)
                                    .SetValidator(new EmployeeDtoValidator());

            RuleFor(prop => prop.AmountPeople)
                                    .GreaterThan(1);

            RuleForEach(prop => prop.DrinksDto)
                                        .SetValidator(new DrinkDtoValidator());

            RuleForEach(prop => prop.DishesDto)
                                        .SetValidator(new DishDtoValidator());

            RuleFor(prop => prop.TotalPrice)
                                    .GreaterThanOrEqualTo(0);

        }
    }
}


using BarSystem.WebApi.DTOs;
using FluentValidation;

namespace BarSystem.WebApi.Validations
{
    public class TableDtoValidator : AbstractValidator<TableDto>
    {
        public TableDtoValidator()
        {
            RuleFor(table => table.WaiterDto)
                                    .SetValidator(new EmployeeDtoValidator());

            RuleFor(table => table.AmountPeople)
                                    .GreaterThan(1);

            RuleForEach(table => table.DrinksDto)
                                        .SetValidator(new DrinkDtoValidator());

            RuleForEach(table => table.DishesDto)
                                        .SetValidator(new DishDtoValidator());

            RuleFor(table => table.TotalPrice)
                                    .GreaterThanOrEqualTo(0).When(prop => prop.DishesDto.Any() || prop.DrinksDto.Any());

        }
    }
}


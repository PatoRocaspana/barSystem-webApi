using BarSystem.WebApi.DTOs;
using FluentValidation;

namespace BarSystem.WebApi.Validations
{
    public class TableDtoValidator : AbstractValidator<TableDto>
    {
        public TableDtoValidator()
        {
            RuleFor(table => table.AmountPeople)
                                    .GreaterThanOrEqualTo(0);

            //RuleFor(table => table.TotalPrice)
            //                        .GreaterThanOrEqualTo(0).When(prop => prop.DishesId.Any() || prop.DrinksId.Any());

        }
    }
}

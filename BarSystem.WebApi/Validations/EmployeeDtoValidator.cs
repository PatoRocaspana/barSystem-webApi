using BarSystem.WebApi.DTOs;
using FluentValidation;

namespace BarSystem.WebApi.Validations
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(employee => employee.Dni)
                        .Cascade(CascadeMode.Stop)
                        .Length(7, 9)
                        .Matches("^[0-9]+$");

            RuleFor(employee => employee.FirstName)
                        .Cascade(CascadeMode.Stop)
                        .NotEmpty()
                        .Length(2, 15);

            RuleFor(employee => employee.LastName)
                        .Cascade(CascadeMode.Stop)
                        .NotEmpty()
                        .Length(1, 25);

            RuleFor(employee => employee.Role)
                                    .IsInEnum();
        }
    }
}

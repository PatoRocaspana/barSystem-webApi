using BarSystem.WebApi.DTOs;
using FluentValidation;

namespace BarSystem.WebApi.Validations
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(prop => prop.Dni)
                        .Cascade(CascadeMode.Stop)
                        .Length(7, 9)
                        .Matches("^[0-9]+$");

            RuleFor(prop => prop.FirstName)
                        .Cascade(CascadeMode.Stop)
                        .NotEmpty()
                        .Length(2, 15);

            RuleFor(prop => prop.LastName)
                        .Cascade(CascadeMode.Stop)
                        .NotEmpty()
                        .Length(1, 25);

            RuleFor(prop => prop.Role)
                                    .IsInEnum();
        }
    }
}

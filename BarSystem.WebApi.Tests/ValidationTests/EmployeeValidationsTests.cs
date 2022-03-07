using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Models.Enum;
using BarSystem.WebApi.Validations;
using FluentValidation.TestHelper;
using Xunit;

namespace BarSystem.WebApi.Tests.ValidationTests
{
    public class EmployeeValidationsTests
    {
        private readonly EmployeeDtoValidator _employeeDtoValidator;
        private readonly EmployeeDto _employeeDto;

        public EmployeeValidationsTests()
        {
            _employeeDto = new EmployeeDto()
            {
                Id = 1,
                Dni = "12345678",
                FirstName = "Johny",
                LastName = "Bravo",
                Role = Role.Admin
            };

            _employeeDtoValidator = new EmployeeDtoValidator();
        }

        [Fact]
        public void Validation_ShouldNotHaveError_HappyPath()
        {
            //Act
            var result = _employeeDtoValidator.TestValidate(_employeeDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(e => e);
        }

        [Theory]
        [InlineData("")]
        [InlineData("2.345.678")]
        [InlineData("A1234567")]
        [InlineData("1234567890")]
        [InlineData("123456")]
        public void Validation_ShouldHaveError_InvalidDni(string dni)
        {
            //Assert
            _employeeDto.Dni = dni;

            //Act
            var result = _employeeDtoValidator.TestValidate(_employeeDto);

            //Assert
            result.ShouldHaveValidationErrorFor(e => e.Dni);
        }

        [Theory]
        [InlineData("")]
        [InlineData("A")]
        [InlineData("ThisIsALoongName")]
        public void Validation_ShouldHaveError_InvalidFirstName(string firstName)
        {
            //Assert
            _employeeDto.FirstName = firstName;

            //Act
            var result = _employeeDtoValidator.TestValidate(_employeeDto);

            //Assert
            result.ShouldHaveValidationErrorFor(e => e.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData("ThisIsAVeryLongLngLastname")]
        public void Validation_ShouldHaveError_InvalidLastName(string lastname)
        {
            //Assert
            _employeeDto.LastName = lastname;

            //Act
            var result = _employeeDtoValidator.TestValidate(_employeeDto);

            //Assert
            result.ShouldHaveValidationErrorFor(e => e.LastName);
        }

        [Fact]
        public void Validation_ShouldHaveError_InvalidRole()
        {
            //Assert
            _employeeDto.Role = (Role)100;

            //Act
            var result = _employeeDtoValidator.TestValidate(_employeeDto);

            //Assert
            result.ShouldHaveValidationErrorFor(e => e.Role);
        }
    }
}

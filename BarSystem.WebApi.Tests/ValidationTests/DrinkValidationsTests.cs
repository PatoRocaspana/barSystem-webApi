using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Models.Enum;
using BarSystem.WebApi.Validations;
using FluentValidation.TestHelper;
using Xunit;

namespace BarSystem.WebApi.Tests.ValidationTests
{
    public class DrinkValidationsTests
    {
        private readonly DrinkDtoValidator _drinkDtoValidator;
        private readonly DrinkDto _drinkDto;

        public DrinkValidationsTests()
        {
            _drinkDto = new DrinkDto()
            {
                Category = (DrinkCategory)5
            };

            _drinkDtoValidator = new DrinkDtoValidator();
        }

        [Fact]
        public void Validation_ShouldHaveError_InvalidEnumCategory()
        {
            //Arrange
            _drinkDto.Category = (DrinkCategory)100;

            //Act
            var result = _drinkDtoValidator.TestValidate(_drinkDto);

            //Assert
            result.ShouldHaveValidationErrorFor(drink => drink.Category);
        }
    }
}

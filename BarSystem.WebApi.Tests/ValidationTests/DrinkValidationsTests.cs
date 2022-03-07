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
            _drinkDto = new DrinkDto() { };

            _drinkDtoValidator = new DrinkDtoValidator();
        }

        [Fact]
        public void Validation_ShouldNotHaveError_DrinkCategoryIsValid()
        {
            //Arrange
            _drinkDto.Category = DrinkCategory.SpecialDrink;

            //Act
            var result = _drinkDtoValidator.TestValidate(_drinkDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(drink => drink.Category);
        }

        [Fact]
        public void Validation_ShouldHaveError_InvalidDrinkCategory()
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

using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Models.Enum;
using BarSystem.WebApi.Validations;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace BarSystem.WebApi.Tests.ValidationTests
{
    public class DishValidationsTests
    {
        private readonly DishDtoValidator _dishDtoValidator;
        private readonly DishDto _dishDto;

        public DishValidationsTests()
        {
            _dishDto = new DishDto()
            {
                EstimatedTime = TimeSpan.FromMinutes(35),
            };

            _dishDtoValidator = new DishDtoValidator();
        }

        [Fact]
        public void Validation_ShouldNotHaveError_DrinkCategoryIsValid()
        {
            //Arrange
            _dishDto.Category = FoodCategory.ElaborateDish;

            //Act
            var result = _dishDtoValidator.TestValidate(_dishDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(dish => dish.Category);
        }

        [Fact]
        public void Validation_ShouldHaveError_InvalidFoodCategory()
        {
            //Arrange
            _dishDto.Category = (FoodCategory)100;

            //Act
            var result = _dishDtoValidator.TestValidate(_dishDto);

            //Assert
            result.ShouldHaveValidationErrorFor(dish => dish.Category);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(151)]
        public void Validation_ShouldHaveError_EstimatedTimeIsOutOfRange(int minutes)
        {
            //Arrange
            _dishDto.EstimatedTime = TimeSpan.FromMinutes(minutes);

            //Act
            var result = _dishDtoValidator.TestValidate(_dishDto);

            //Assert
            result.ShouldHaveValidationErrorFor(dish => dish.EstimatedTime)
                  .WithErrorMessage("Estimated Time must be between 0 and 150 minutes");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(150)]
        public void Validation_ShouldNotHaveError_EstimatedTimeIsWithinRange(int minutes)
        {
            //Arrange
            _dishDto.EstimatedTime = TimeSpan.FromMinutes(minutes);

            //Act
            var result = _dishDtoValidator.TestValidate(_dishDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(dish => dish.EstimatedTime);
        }
    }
}

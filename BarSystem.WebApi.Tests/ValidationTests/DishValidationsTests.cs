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
        private DishDto _sampleDishDto;

        public DishValidationsTests()
        {
            _sampleDishDto = new DishDto()
            {
                Name = "Crazy Steak",
                Description = "Steak with Mushroom Sauce",
                Price = 100,
                Stock = 10,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(35),
                IsReady = false

            };

            _dishDtoValidator = new DishDtoValidator();
        }

        [Fact]
        public void Validation_ShouldHaveError_NameIsEmpty()
        {
            //Arrange
            _sampleDishDto.Name = string.Empty;

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldHaveValidationErrorFor(dish => dish.Name);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("This is a very long name for a Dish")]
        public void Validation_ShouldHaveError_NameLengthIsOutOfRange(string Name)
        {
            //Arrange
            _sampleDishDto.Name = Name;

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldHaveValidationErrorFor(dish => dish.Name);
        }

        [Fact]
        public void Validation_ShouldNotHaveError_NameIsValid()
        {
            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(dish => dish.Name);
        }

        [Fact]
        public void Validation_ShouldHaveError_DescriptionLengthIsOver200()
        {
            //Arrange
            _sampleDishDto.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer sapien elit, dapibus eu auctor in, commodo quis leo. " +
                                         "Nulla sagittis consequat pellentesque. Vivamus tempus tortor at est lacinia, ut tempor lacus pellentesque";

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldHaveValidationErrorFor(dish => dish.Description);
        }

        [Theory]
        [InlineData("")]
        [InlineData("This is a normal description")]
        public void Validation_ShouldNotHaveError_DescriptionIsEmptyOrInRange(string description)
        {
            //Arrange
            _sampleDishDto.Description = description;

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(dish => dish.Description);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validation_ShouldHaveError_PriceIsLessOrEqualThanZero(int price)
        {
            //Arrange
            _sampleDishDto.Price = price;

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldHaveValidationErrorFor(dish => dish.Price);
        }

        [Fact]
        public void Validation_ShouldNotHaveError_PriceisOver100()
        {
            //Arrange
            _sampleDishDto.Price = 1;

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(dish => dish.Price);
        }

        [Fact]
        public void Validation_ShouldHaveError_StockIsLessThanZero()
        {
            //Arrange
            _sampleDishDto.Stock = -1;

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldHaveValidationErrorFor(dish => dish.Stock);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Validation_ShouldNotHaveError_StockIsEqualOrMoreThanZero(int stock)
        {
            //Arrange
            _sampleDishDto.Stock = stock;

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(dish => dish.Stock);
        }

        [Fact]
        public void Validation_ShouldHaveError_InvalidEnumCategory()
        {
            //Arrange
            _sampleDishDto.Category = (FoodCategory)100;

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldHaveValidationErrorFor(dish => dish.Category);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(151)]
        public void Validation_ShouldHaveError_EstimatedTimeIsOutOfRange(int minutes)
        {
            //Arrange
            _sampleDishDto.EstimatedTime = TimeSpan.FromMinutes(minutes);

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldHaveValidationErrorFor(dish => dish.EstimatedTime)
                  .WithErrorMessage("Estimated Time must be between 0 and 150 minutes");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(150)]
        public void Validation_ShouldNotHaveError_EstimatedTimeIsInOfRange(int minutes)
        {
            //Arrange
            _sampleDishDto.EstimatedTime = TimeSpan.FromMinutes(minutes);

            //Act
            var result = _dishDtoValidator.TestValidate(_sampleDishDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(dish => dish.EstimatedTime);
        }
    }
}

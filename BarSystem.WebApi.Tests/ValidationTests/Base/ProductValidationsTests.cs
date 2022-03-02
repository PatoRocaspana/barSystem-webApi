using BarSystem.WebApi.DTOs.Base;
using BarSystem.WebApi.Validations.Base;
using FluentValidation.TestHelper;
using Xunit;

namespace BarSystem.WebApi.Tests.ValidationTests.Base
{
    public class ProductValidationsTests
    {

        public class Product : ProductDto { }
        public class Validator : ProductDtoValidator<Product> { }

        private readonly Validator _validator;
        private readonly Product _product;

        public ProductValidationsTests()
        {
            _validator = new Validator();
            _product = new Product()
            {
                Id = 1,
                Name = "ProductName",
                Description = "Description of the product",
                Price = 10,
                Stock = 1,
            };
        }


        [Fact]
        public void Validation_ShouldHaveError_NegativeId()
        {
            //Arrange
            _product.Id = -1;

            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldHaveValidationErrorFor(product => product.Id);
        }

        [Fact]
        public void Validation_ShouldHaveError_NameIsEmpty()
        {
            //Arrange
            _product.Name = string.Empty;

            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        [Theory]
        [InlineData("A")]
        [InlineData("This is a very long name for a Drink")]
        public void Validation_ShouldHaveError_NameLengthIsOutOfRange(string Name)
        {
            //Arrange
            _product.Name = Name;

            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldHaveValidationErrorFor(product => product.Name);
        }

        [Fact]
        public void Validation_ShouldNotHaveError_NameIsValid()
        {
            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldNotHaveValidationErrorFor(product => product.Name);
        }

        [Fact]
        public void Validation_ShouldHaveError_DescriptionLengthIsOver200()
        {
            //Arrange
            _product.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer sapien elit, dapibus eu auctor in, commodo quis leo. " +
                                         "Nulla sagittis consequat pellentesque. Vivamus tempus tortor at est lacinia, ut tempor lacus pellentesque";

            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldHaveValidationErrorFor(drink => drink.Description);
        }

        [Theory]
        [InlineData("")]
        [InlineData("This is a normal description")]
        public void Validation_ShouldNotHaveError_DescriptionIsEmptyOrInRange(string description)
        {
            //Arrange
            _product.Description = description;

            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldNotHaveValidationErrorFor(product => product.Description);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Validation_ShouldHaveError_PriceIsLessOrEqualThanZero(int price)
        {
            //Arrange
            _product.Price = price;

            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldHaveValidationErrorFor(product => product.Price);
        }

        [Fact]
        public void Validation_ShouldNotHaveError_PriceisMoreThanZero()
        {
            //Arrange
            _product.Price = 1;

            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldNotHaveValidationErrorFor(product => product.Price);
        }

        [Fact]
        public void Validation_ShouldHaveError_StockIsLessThanZero()
        {
            //Arrange
            _product.Stock = -1;

            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldHaveValidationErrorFor(product => product.Stock);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void Validation_ShouldNotHaveError_StockIsEqualOrMoreThanZero(int stock)
        {
            //Arrange
            _product.Stock = stock;

            //Act
            var result = _validator.TestValidate(_product);

            //Assert
            result.ShouldNotHaveValidationErrorFor(product => product.Stock);
        }
    }
}

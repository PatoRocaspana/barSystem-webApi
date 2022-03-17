using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Models.Enum;
using BarSystem.WebApi.Validations;
using FluentValidation.TestHelper;
using System.Collections.Generic;
using Xunit;

namespace BarSystem.WebApi.Tests.ValidationTests
{
    public class TableValidationTests
    {
        private readonly TableDtoValidator _tableDtoValidator;
        private readonly TableDto _tableeDto;

        public TableValidationTests()
        {
            _tableeDto = new TableDto()
            {
                Id = 1,
                EmployeeId = 1,
                AmountPeople = 1,
                ExistAdult = true,
                DishIds = new List<int>() { 1, 2},
                DrinksIds = new List<int>() { 1, 2 }
            };

            _tableDtoValidator = new TableDtoValidator();
        }


        [Fact]
        public void Validation_ShouldHaveError_AmountPeopleLessThanZero()
        {
            //Assert
            _tableeDto.AmountPeople = -1;

            //Act
            var result = _tableDtoValidator.TestValidate(_tableeDto);

            //Assert
            result.ShouldHaveValidationErrorFor(e => e.AmountPeople);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void Validation_ShouldNotHaveError_AmountPeopleGreaterOrEqualThanZero(int amountPeople)
        {
            //Assert
            _tableeDto.AmountPeople = amountPeople;

            //Act
            var result = _tableDtoValidator.TestValidate(_tableeDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(e => e.AmountPeople);
        }
    }
}

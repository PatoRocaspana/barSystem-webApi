using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Models.Enum;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace BarSystem.WebApi.Tests.IntegrationTests
{
    public class DrinkControllerIntegrationTests : IClassFixture<BarSystemWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly DrinkDto _drinkDto;

        public DrinkControllerIntegrationTests(BarSystemWebAppFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();

            _drinkDto = new DrinkDto()
            {
                Id = 0,
                Name = "CocaCola",
                Description = "Coca Cola 330cc",
                Price = 100,
                Stock = 10,
                Category = DrinkCategory.Soda,
            };
        }

        [Fact]
        public async Task Get_ReturnsOkStatusCode_WhenDrinksExists()
        {
            //Act
            var response = await _httpClient.GetAsync("/api/Drink");
            var result = await response.Content.ReadFromJsonAsync<List<DrinkDto>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task Get_ReturnsOkStatusCode_WhenDrinkExists()
        {
            //Act
            var response = await _httpClient.GetAsync("/api/Drink/2");
            var result = await response.Content.ReadFromJsonAsync<DrinkDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal("DrinkB", result.Name);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task Get_ReturnsNotFoundStatusCode_WhenDrinkNoExists()
        {
            //Act
            var response = await _httpClient.GetAsync("/api/Drink/9");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_ReturnsCreatedStatusCode_WhenSuccess()
        {
            //Act
            var response = await _httpClient.PostAsJsonAsync("/api/Drink", _drinkDto);
            var result = await response.Content.ReadFromJsonAsync<DrinkDto>();

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal(_drinkDto.Name, result.Name);
        }

        [Fact]
        public async Task Put_ReturnsOkStatusCode_WhenSuccess()
        {
            //Arrange
            _drinkDto.Id = 1;

            //Act
            var response = await _httpClient.PutAsJsonAsync("/api/Drink/1", _drinkDto);
            var result = await response.Content.ReadFromJsonAsync<DrinkDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal(_drinkDto.Name, result.Name);
            Assert.Equal(_drinkDto.Id, result.Id);
        }

        [Fact]
        public async Task Put_ReturnsBadRequestStatusCode_WhenIdsNoMatch()
        {
            //Arrange
            _drinkDto.Id = 2;

            //Act
            var response = await _httpClient.PutAsJsonAsync("/api/Drink/1", _drinkDto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_ReturnsNotFoundStatusCode_WhenIdsNoExists()
        {
            //Arrange
            _drinkDto.Id = 4;

            //Act
            var response = await _httpClient.PutAsJsonAsync("/api/Drink/4", _drinkDto);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentStatusCode_WhenSuccess()
        {
            //Act
            var response = await _httpClient.DeleteAsync("/api/Drink/3");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundStatusCode_WhenIdNoExists()
        {
            //Act
            var response = await _httpClient.DeleteAsync("/api/Drink/5");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

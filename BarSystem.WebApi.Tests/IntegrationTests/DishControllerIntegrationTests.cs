using BarSystem.WebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace BarSystem.WebApi.Tests.IntegrationTests
{
    public class DishControllerIntegrationTests : IClassFixture<BarSystemWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly DishDto _dishDto;

        public DishControllerIntegrationTests(BarSystemWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();

            _dishDto = new DishDto()
            {
                Id = 0,
                Name = "Crazy Steak",
                Description = "Steak with Mushroom Sauce",
                Price = 100,
                Stock = 10,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(35),
                IsReady = false
            };
        } 
            
        [Fact]
        public async Task Get_ReturnsOkStatusCode_WhenDishesExists()
        {
            //Act
            var response = await _client.GetAsync("/api/Dish");
            var result = await response.Content.ReadFromJsonAsync<List<DishDto>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Get_ReturnsOkStatusCode_WhenDishExists()
        {
            //Act
            var response = await _client.GetAsync("/api/Dish/2");
            var result = await response.Content.ReadFromJsonAsync<DishDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal("NameB", result.Name);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task Get_ReturnsNotFoundStatusCode_WhenDishNoExists()
        {
            //Act
            var response = await _client.GetAsync("/api/Dish/3");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_ReturnsCreatedStatusCode_WhenSuccess()
        {
            //Act
            var response = await _client.PostAsJsonAsync("/api/Dish", _dishDto);
            var result = await response.Content.ReadFromJsonAsync<DishDto>();

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal("Crazy Steak", result.Name);
        }

        [Fact]
        public async Task Put_ReturnsOkStatusCode_WhenSuccess()
        {
            //Arrange
            _dishDto.Id = 2;

            //Act
            var response = await _client.PutAsJsonAsync("/api/Dish/2", _dishDto);
            var result = await response.Content.ReadFromJsonAsync<DishDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal("Crazy Steak", result.Name);
            Assert.Equal(_dishDto.Id, result.Id);
        }

        [Fact]
        public async Task Put_ReturnsBadRequestStatusCode_WhenIdsNoMatch()
        {
            //Arrange
            _dishDto.Id = 2;

            //Act
            var response = await _client.PutAsJsonAsync("/api/Dish/1", _dishDto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_ReturnsNotFoundStatusCode_WhenIdsNoExists()
        {
            //Arrange
            _dishDto.Id = 4;

            //Act
            var response = await _client.PutAsJsonAsync("/api/Dish/4", _dishDto);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentStatusCode_WhenSuccess()
        {
            //Act
            var response = await _client.DeleteAsync("/api/Dish/2");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundStatusCode_WhenIdNoExists()
        {
            //Act
            var response = await _client.DeleteAsync("/api/Dish/5");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

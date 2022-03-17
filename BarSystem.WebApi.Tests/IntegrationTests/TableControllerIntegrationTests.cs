using BarSystem.WebApi.DTOs;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace BarSystem.WebApi.Tests.IntegrationTests
{
    public class TableControllerIntegrationTests : IClassFixture<BarSystemWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly TableDto _tableDto;

        public TableControllerIntegrationTests(BarSystemWebAppFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();

            _tableDto = new TableDto()
            {
                AmountPeople = 5,
                EmployeeId = 1,
                DrinksIds = new List<int>(1),
                DishIds = new List<int>(1)
            };
        }

        [Fact]
        public async Task Get_ReturnsOkStatusCode_WhenTablesExists()
        {
            //Act
            var response = await _httpClient.GetAsync("/api/Table");
            var result = await response.Content.ReadFromJsonAsync<List<TableInfoDto>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task Get_ReturnsOkStatusCode_WhenTableExists()
        {
            //Act
            var response = await _httpClient.GetAsync("/api/Table/33");
            var result = await response.Content.ReadFromJsonAsync<TableInfoDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal(33, result.Id);
            Assert.Equal(1, result.EmployeeId);
            Assert.Equal(30, result.AmountPeople);
        }

        [Fact]
        public async Task Get_ReturnsNotFoundStatusCode_WhenTableNoExists()
        {
            //Act
            var response = await _httpClient.GetAsync("/api/Table/9");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_ReturnsCreatedStatusCode_WhenSuccess()
        {
            //Act
            var response = await _httpClient.PostAsJsonAsync("/api/Table", _tableDto);
            var result = await response.Content.ReadFromJsonAsync<TableInfoDto>();

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal(_tableDto.AmountPeople, result.AmountPeople);
        }

        [Fact]
        public async Task Put_ReturnsOkStatusCode_WhenSuccess()
        {
            //Arrange
            _tableDto.Id = 1;

            //Act
            var response = await _httpClient.PutAsJsonAsync("/api/Table/1", _tableDto);
            var result = await response.Content.ReadFromJsonAsync<TableInfoDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal(_tableDto.AmountPeople, result.AmountPeople);
            Assert.Equal(_tableDto.Id, result.Id);
        }

        [Fact]
        public async Task Put_ReturnsBadRequestStatusCode_WhenIdsNoMatch()
        {
            //Arrange
            _tableDto.Id = 2;

            //Act
            var response = await _httpClient.PutAsJsonAsync("/api/Table/1", _tableDto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_ReturnsNotFoundStatusCode_WhenIdsNoExists()
        {
            //Arrange
            _tableDto.Id = 7;

            //Act
            var response = await _httpClient.PutAsJsonAsync("/api/Table/7", _tableDto);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentStatusCode_WhenSuccess()
        {
            //Act
            var response = await _httpClient.DeleteAsync("/api/Table/2");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundStatusCode_WhenIdNoExists()
        {
            //Act
            var response = await _httpClient.DeleteAsync("/api/Table/7");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

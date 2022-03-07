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
    public class EmployeeControllerIntegrationTests : IClassFixture<BarSystemWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly EmployeeDto _employeeDto;

        public EmployeeControllerIntegrationTests(BarSystemWebAppFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();

            _employeeDto = new EmployeeDto()
            {
                Id = 0,
                FirstName = "John",
                LastName = "Travolta",
                Dni = "11222333",
                Role = Role.Barman
            };
        }

        [Fact]
        public async Task Get_ReturnsOkStatusCode_WhenEmployeesExists()
        {
            //Act
            var response = await _httpClient.GetAsync("/api/Employee");
            var result = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task Get_ReturnsOkStatusCode_WhenEmployeeExists()
        {
            //Act
            var response = await _httpClient.GetAsync("/api/Employee/2");
            var result = await response.Content.ReadFromJsonAsync<EmployeeDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal("EmployeeB", result.FirstName);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task Get_ReturnsNotFoundStatusCode_WhenEmployeeNoExists()
        {
            //Act
            var response = await _httpClient.GetAsync("/api/Employee/9");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_ReturnsCreatedStatusCode_WhenSuccess()
        {
            //Act
            var response = await _httpClient.PostAsJsonAsync("/api/Employee", _employeeDto);
            var result = await response.Content.ReadFromJsonAsync<EmployeeDto>();

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal(_employeeDto.FirstName, result.FirstName);
        }

        [Fact]
        public async Task Put_ReturnsOkStatusCode_WhenSuccess()
        {
            //Arrange
            _employeeDto.Id = 1;

            //Act
            var response = await _httpClient.PutAsJsonAsync("/api/Employee/1", _employeeDto);
            var result = await response.Content.ReadFromJsonAsync<EmployeeDto>();

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.NotNull(result);
            Assert.Equal(_employeeDto.FirstName, result.FirstName);
            Assert.Equal(_employeeDto.Id, result.Id);
        }

        [Fact]
        public async Task Put_ReturnsBadRequestStatusCode_WhenIdsNoMatch()
        {
            //Arrange
            _employeeDto.Id = 2;

            //Act
            var response = await _httpClient.PutAsJsonAsync("/api/Employee/1", _employeeDto);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_ReturnsNotFoundStatusCode_WhenIdsNoExists()
        {
            //Arrange
            _employeeDto.Id = 4;

            //Act
            var response = await _httpClient.PutAsJsonAsync("/api/Employee/4", _employeeDto);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNoContentStatusCode_WhenSuccess()
        {
            //Act
            var response = await _httpClient.DeleteAsync("/api/Employee/3");

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundStatusCode_WhenIdNoExists()
        {
            //Act
            var response = await _httpClient.DeleteAsync("/api/Employee/5");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

using BarSystem.WebApi.Controllers;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BarSystem.WebApi.Tests.ControllersTests
{
    public class DishControllerTests
    {
        private readonly Mock<IDishRepository> _mockDishRepository;
        private readonly DishController _dishController;

        public DishControllerTests()
        {
            _mockDishRepository = new Mock<IDishRepository>();
            _dishController = new DishController(_mockDishRepository.Object);
        }

        [Fact]
        public async Task Get_ReturnsDishDtoList_WhenDishesExists()
        {
            //Arrange
            var dish = new Dish()
            {
                Name = "Crazy Steak",
                Description = "Steak with Mushroom Sauce",
                Price = 100,
                Stock = 10,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(35),
                IsReady = false
            };

            var dishList = new List<Dish>
            {
                dish,
                dish,
                dish
            };

            _mockDishRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(dishList);

            //Act
            var result = await _dishController.Get();

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var listResult = Assert.IsType<List<DishDto>>(objectResult.Value);
            Assert.Equal(dishList.Count, listResult.Count);
            Assert.Equal(dishList[0].Name, listResult[0].Name);
            _mockDishRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsEmptyDishDtoList_WhenNoDishes()
        {
            //Arrange
            var dishList = new List<Dish>();

            _mockDishRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(dishList);

            //Act
            var result = await _dishController.Get();

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var listResult = Assert.IsType<List<DishDto>>(objectResult.Value);
            Assert.Empty(listResult);
            _mockDishRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsDishDto_WhenDishExists()
        {
            //Arrange
            var dish = new Dish()
            {
                Name = "Crazy Steak",
                Description = "Steak with Mushroom Sauce",
                Price = 100,
                Stock = 10,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(35),
                IsReady = false
            };

            _mockDishRepository.Setup(r => r.GetAsync(dish.Id)).ReturnsAsync(dish);

            //Act
            var result = await _dishController.Get(dish.Id);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var dishResult = Assert.IsType<DishDto>(objectResult.Value);
            Assert.Equal(dish.Name, dishResult.Name);
            Assert.Equal(dish.Id, dishResult.Id);
            _mockDishRepository.Verify(r => r.GetAsync(dish.Id), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenDishNoExists()
        {
            //arrange
            var dishId = 2;

            _mockDishRepository.Setup(r => r.GetAsync(dishId)).ReturnsAsync((Dish)null);

            //act
            var result = await _dishController.Get(dishId);

            //assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<NotFoundResult>(result);
            _mockDishRepository.Verify(r => r.GetAsync(dishId), Times.Never);
        }

        [Fact]
        public async Task Post_ReturnsDishDto_WhenSucces()
        {
            //arrange
            var dishDto = new DishDto()
            {
                Name = "Crazy Steak",
                Description = "Steak with Mushroom Sauce",
                Price = 100,
                Stock = 10,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(35),
                IsReady = false
            };

            var dish = new Dish()
            {
                Id = 1,
                Name = "Crazy Steak",
                Description = "Steak with Mushroom Sauce",
                Price = 100,
                Stock = 10,
                Category = 0,
                EstimatedTime = TimeSpan.FromMinutes(35),
                IsReady = false
            };

            _mockDishRepository.Setup(r => r.CreateAsync(It.Is<Dish>(d => d.Id == dish.Id))).ReturnsAsync(dish);

            //act
            var result = await _dishController.Post(dishDto);

            //assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var dishDtoCreated = Assert.IsType<DishDto>(objectResult.Value);
            Assert.Equal(dishDto.Id, dishDtoCreated.Id);
            _mockDishRepository.Verify(r => r.CreateAsync(It.Is<Dish>(d => d.Id == dish.Id)), Times.Once);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenErrors()
        {
            //arrange
            var dishDto = new DishDto();

            _mockDishRepository.Setup(r => r.CreateAsync(It.IsAny<Dish>())).ReturnsAsync((Dish)null);

            //act
            var result = await _dishController.Post(dishDto);

            //assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<BadRequestResult>(result);
            _mockDishRepository.Verify(r => r.CreateAsync(It.IsAny<Dish>()), Times.Once);
        }
    }
}

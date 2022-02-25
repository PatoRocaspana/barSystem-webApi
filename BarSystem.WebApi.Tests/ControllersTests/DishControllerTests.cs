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
            };

            var dishList = new List<Dish>
            {
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
                Id = 1,
                Name = "Crazy Steak",
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
            //Arrange
            var dishId = 2;

            _mockDishRepository.Setup(r => r.GetAsync(dishId)).ReturnsAsync((Dish)null);

            //Act
            var result = await _dishController.Get(dishId);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<NotFoundResult>(result);
            _mockDishRepository.Verify(r => r.GetAsync(dishId), Times.Once);
        }

        [Fact]
        public async Task Post_ReturnsDishDto_WhenSucces()
        {
            //Arrange
            var dishDto = new DishDto()
            {
                Name = "Crazy Steak",
            };

            var dish = new Dish()
            {
                Id = 1,
                Name = "Crazy Steak",
            };

            _mockDishRepository.Setup(r => r.CreateAsync(It.Is<Dish>(d => d.Name == dishDto.Name))).ReturnsAsync(dish);

            //Act
            var result = await _dishController.Post(dishDto);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<CreatedAtActionResult>(result);
            var dishDtoCreated = Assert.IsType<DishDto>(objectResult.Value);
            Assert.Equal(dish.Id, dishDtoCreated.Id);
            Assert.Equal(dish.Name, dishDtoCreated.Name);
            _mockDishRepository.Verify(r => r.CreateAsync(It.IsAny<Dish>()), Times.Once);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenErrors()
        {
            //Arrange
            var dishDto = new DishDto();

            _mockDishRepository.Setup(r => r.CreateAsync(It.IsAny<Dish>())).ReturnsAsync((Dish)null);

            //Act
            var result = await _dishController.Post(dishDto);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<BadRequestResult>(result);
            _mockDishRepository.Verify(r => r.CreateAsync(It.IsAny<Dish>()), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsUpdatedDishDto_WhenExists()
        {
            //Arrange
            var dishDto = new DishDto()
            {
                Id = 1,
                Name = "Storm Barbacue",
            };

            var dish = new Dish()
            {
                Id = 1,
                Name = "Storm Barbacue",
            };

            _mockDishRepository.Setup(r => r.UpdateAsync(It.Is<Dish>(d => d.Id == dish.Id), dish.Id))
                                            .ReturnsAsync(dish);
            _mockDishRepository.Setup(r => r.EntityExistsAsync(dish.Id)).ReturnsAsync(true);

            //Act
            var result = await _dishController.Put(dishDto, dishDto.Id);

            //Assert
            Assert.NotNull(result);
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var dishDtoUpdated = Assert.IsType<DishDto>(objectResult.Value);
            Assert.Equal(dishDto.Id, dishDtoUpdated.Id);
            Assert.Equal(dishDto.Name, dishDtoUpdated.Name);
            _mockDishRepository.Verify(r => r.UpdateAsync(It.Is<Dish>(d => d.Id == dish.Id), dish.Id), Times.Once);
            _mockDishRepository.Verify(r => r.EntityExistsAsync(dish.Id), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenNoExists()
        {
            //Arrange
            var dishDto = new DishDto()
            {
                Id = 1,
                Name = "Storm Barbacue",
            };

            _mockDishRepository.Setup(r => r.UpdateAsync(It.Is<Dish>(cl => cl.Id == dishDto.Id), dishDto.Id)).ReturnsAsync(new Dish());
            _mockDishRepository.Setup(r => r.EntityExistsAsync(dishDto.Id)).ReturnsAsync(false);

            //Act
            var resultQuery = await _dishController.Put(dishDto, dishDto.Id);

            //Assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<NotFoundResult>(resultQuery);
            _mockDishRepository.Verify(r => r.EntityExistsAsync(dishDto.Id), Times.Once);
            _mockDishRepository.Verify(r => r.UpdateAsync(It.Is<Dish>(cl => cl.Id == dishDto.Id), dishDto.Id), Times.Never);
        }

        [Fact]
        public async Task Put_ReturnsNotFound_WhenIdsNoMatch()
        {
            //Arrange
            var dishDto = new DishDto()
            {
                Id = 1,
                Name = "Storm Barbacue",
            };

            var otherId = 2;

            _mockDishRepository.Setup(r => r.UpdateAsync(It.Is<Dish>(cl => cl.Id == dishDto.Id), otherId)).ReturnsAsync(new Dish());
            _mockDishRepository.Setup(r => r.EntityExistsAsync(dishDto.Id)).ReturnsAsync(true);

            //Act
            var resultQuery = await _dishController.Put(dishDto, otherId);

            //Assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<BadRequestResult>(resultQuery);
            _mockDishRepository.Verify(r => r.EntityExistsAsync(dishDto.Id), Times.Never);
            _mockDishRepository.Verify(r => r.UpdateAsync(It.Is<Dish>(cl => cl.Id == dishDto.Id), otherId), Times.Never);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            //arrange
            var dishId = 1;

            _mockDishRepository.Setup(r => r.EntityExistsAsync(dishId)).ReturnsAsync(true);
            _mockDishRepository.Setup(r => r.DeleteAsync(dishId));

            //act
            var resultQuery = await _dishController.Delete(dishId);

            //assert
            Assert.NotNull(resultQuery);
            var objectResult = Assert.IsType<NoContentResult>(resultQuery);
            _mockDishRepository.Verify(r => r.EntityExistsAsync(dishId), Times.Once);
            _mockDishRepository.Verify(r => r.DeleteAsync(dishId), Times.Once);
        }
    }
}

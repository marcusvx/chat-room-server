using ChatRoomServer.Domain.Repositories;
using ChatRoomServer.WebApi.Controllers;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Collections.Generic;
using ChatRoomService.Domain.Models;
using ChatRoomServer.WebApi.Models;
using FluentAssertions;
using System.Linq;

namespace ChatRoomServer.WebApi.Tests
{
    public class CategoryControllerTest
    {
        private CategoryController controller;
        private Mock<ICategoryRepository> categoryRepositoryMock;
        private Mock<ILogger<CategoryController>> loggerMock;
        private Mock<IMapper> mapperMock;

        private static readonly IEnumerable<Category> AllCategories = new[]
        {
            new Category
            {
                Id  = 9999,
                Name = "Category 1"
            },
            new Category {
                Id = 8888,
                Name = "Category 2"
            }
        };

        public CategoryControllerTest()
        {
            this.categoryRepositoryMock = new Mock<ICategoryRepository>();
            this.loggerMock = new Mock<ILogger<CategoryController>>();
            this.mapperMock = new Mock<IMapper>();

            this.categoryRepositoryMock.Setup(c => c.GetAll()).Returns(AllCategories);

            this.controller = new CategoryController(
                this.loggerMock.Object,
                this.categoryRepositoryMock.Object,
                this.mapperMock.Object);
        }

        [Fact]
        public void Get_Success_ShouldReturnAllCategories()
        {
            // arrange
            var expected = AllCategories
                .Select(c => new CategoryResponse { Id = c.Id, Name = c.Name })
                .ToArray();

            this.mapperMock
                .Setup(mapper => mapper.Map<CategoryResponse[]>(AllCategories))
                .Returns(expected);

            // act
            var result = this.controller.Get();

            // assert
            result.Should().ContainInOrder(expected);
        }
    }
}

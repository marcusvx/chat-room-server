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
    public class RoomControllerTest
    {
        private RoomController controller;
        private Mock<IRoomRepository> categoryRepositoryMock;
        private Mock<ILogger<RoomController>> loggerMock;
        private Mock<IMapper> mapperMock;

        private static readonly IEnumerable<Room> AllCategories = new[]
        {
            new Room
            {
                Id  = 9999,
                Name = "Category 1"
            },
            new Room {
                Id = 8888,
                Name = "Category 2"
            }
        };

        public RoomControllerTest()
        {
            this.categoryRepositoryMock = new Mock<IRoomRepository>();
            this.loggerMock = new Mock<ILogger<RoomController>>();
            this.mapperMock = new Mock<IMapper>();

            this.categoryRepositoryMock.Setup(c => c.GetAll()).Returns(AllCategories);

            this.controller = new RoomController(
                this.loggerMock.Object,
                this.categoryRepositoryMock.Object,
                this.mapperMock.Object);
        }

        [Fact]
        public void Get_Success_ShouldReturnAllCategories()
        {
            // arrange
            var expected = AllCategories
                .Select(c => new RoomResponse { Id = c.Id, Name = c.Name })
                .ToArray();

            this.mapperMock
                .Setup(mapper => mapper.Map<RoomResponse[]>(AllCategories))
                .Returns(expected);

            // act
            var result = this.controller.Get();

            // assert
            result.Should().ContainInOrder(expected);
        }
    }
}

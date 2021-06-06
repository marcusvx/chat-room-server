using ChatRoomServer.Domain.Repositories;
using ChatRoomServer.WebApi.Controllers;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System;
using ChatRoomService.Domain.Models;
using System.Collections.Generic;
using ChatRoomServer.WebApi.Models;
using FluentAssertions;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoomServer.WebApi.Tests
{
    public class EventControllerTest
    {
        private EventController controller;
        private Mock<IEventRepository> eventRepositoryMock;
        private Mock<ILogger<EventController>> loggerMock;
        private Mock<IMapper> mapperMock;

        private static EventSummary[] EventsSummaries = new[] {
                 new EventSummary(EventType.Enter, 13, 2, 0),
                 new EventSummary(EventType.Comment, 13, 8, 0),
                 new EventSummary(EventType.HighFive, 13, 2, 3),
                 new EventSummary(EventType.Leave, 13, 1, 0),
                 new EventSummary(EventType.Comment, 14, 5, 0),
                 new EventSummary(EventType.HighFive, 14, 1, 1),
                 new EventSummary(EventType.Leave, 14, 1, 0),
            };

        public EventControllerTest()
        {
            this.eventRepositoryMock = new Mock<IEventRepository>();
            this.loggerMock = new Mock<ILogger<EventController>>();
            this.mapperMock = new Mock<IMapper>();

            this.controller = new EventController(
                this.loggerMock.Object,
                this.eventRepositoryMock.Object,
                this.mapperMock.Object);
        }

        [Fact]
        public void GetHourlySummary_Success_ReturnsSummarizedEvents()
        {
            // arrange
            int year = 2020;
            int month = 11;
            int day = 12;
            int roomId = 1;

            this.eventRepositoryMock.Setup(m => m.GetHourlySummary(new DateTime(year, month, day), roomId)).Returns(EventsSummaries);
            var expected = EventsSummaries.GroupBy(s => s.EventHour).Select(group => new HourlyEventSummaryResponse
            {
                Hour = group.Key,
                Items = group.Select(item => new HourlyEventSummaryItem
                {
                    EventType = item.EventType.ToString(),
                    EventCount = item.EventCount,
                    UserCount = item.UserCount
                })
            }).ToArray();

            this.mapperMock.Setup(m => m.Map<IEnumerable<EventSummary>, HourlyEventSummaryResponse[]>(EventsSummaries)).Returns(expected);

            // act
            var result = this.controller.GetHourlySummary(year, month, day, roomId);

            // expect
            result.Should().BeOfType<OkObjectResult>();
            var actualResult = (result as OkObjectResult).Value as HourlyEventSummaryResponse[];
            actualResult.Should().Equal(expected);
        }

        [Fact]
        public void GetHourlySummary_NoRoomPassed_ReturnsBadRequestResult()
        {
            // arrange
            int year = 2020;
            int month = 11;
            int day = 12;

            // act
            var result = this.controller.GetHourlySummary(year, month, day, null);

            // expect
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}

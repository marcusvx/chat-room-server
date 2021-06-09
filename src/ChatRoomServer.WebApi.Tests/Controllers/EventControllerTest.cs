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

        private static User UserJane = new User(33, "Jane");
        private static User UserJohn = new User(22, "Jane");
        private static Room RoomOne = new Room(1, "Room One");

        private static DateTimeOffset AugustFive2020 = new DateTimeOffset(new DateTime(2020, 9, 5));

        private static Event[] Events = new[] {
            new Event(1, UserJane, null, AugustFive2020.AddMinutes(1), RoomOne, EventType.Enter, null),
            new Event(2, UserJohn, null, AugustFive2020.AddMinutes(2), RoomOne, EventType.Enter, null),
            new Event(3, UserJohn, null, AugustFive2020.AddMinutes(4), RoomOne, EventType.Comment, "Lorem Ipsum"),
            new Event(4, UserJane, null,  AugustFive2020.AddMinutes(5), RoomOne, EventType.Comment, "Dolor Sit Amet"),
            new Event(5, UserJane, UserJohn, AugustFive2020.AddMinutes(6), RoomOne, EventType.HighFive, null),
            new Event(6, UserJane, null, AugustFive2020.AddMinutes(8), RoomOne, EventType.Leave, null),
            new Event(7, UserJohn, null, AugustFive2020.AddMinutes(9), RoomOne, EventType.Leave, null),
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
            int timezoneOffset = -3;
            int roomId = 1;

            var dateTimeOffset = new DateTimeOffset(new DateTime(year, month, day), new TimeSpan(timezoneOffset, 0, 0));
            this.eventRepositoryMock.Setup(m => m.GetHourlySummary(dateTimeOffset, roomId)).Returns(EventsSummaries);
            var expected = EventsSummaries.GroupBy(s => s.EventHour).Select(group => new HourlyEventSummaryResponse
            (
                hour: group.Key,
                items: group.Select(item => new HourlyEventSummaryItem
                (
                    eventType: item.EventType.ToString(),
                    eventCount: item.EventCount,
                    userCount: item.UserCount
                ))
            )).ToArray();

            this.mapperMock.Setup(m => m.Map<IEnumerable<EventSummary>, HourlyEventSummaryResponse[]>(EventsSummaries)).Returns(expected);

            // act
            var result = this.controller.GetHourlySummary(year, month, day, roomId, timezoneOffset);

            // expect
            result.Should().BeOfType<OkObjectResult>();
            var actualResult = (result as OkObjectResult).Value as HourlyEventSummaryResponse[];
            actualResult.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetHourlySummary_NoRoomPassed_ReturnsBadRequestResult()
        {
            // act
            var result = this.controller.GetHourlySummary(2021, 3, 4, null);

            // expect
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Get_Success_ReturnsEvents()
        {
            // arrange
            int year = 2020;
            int month = 9;
            int day = 5;
            int roomId = 2;
            int timezoneOffset = 5;

            var dateTimeOffset = new DateTimeOffset(new DateTime(year, month, day), new TimeSpan(timezoneOffset, 0, 0));
            this.eventRepositoryMock.Setup(m => m.GetEvents(dateTimeOffset, roomId)).Returns(Events);
            var expected = new EventResponse[] {
                new EventResponse(1, Events[0].ReceivedAt, RoomOne.Name, UserJane.Name, null, "Enter", null),
                new EventResponse(2, Events[1].ReceivedAt, RoomOne.Name, UserJohn.Name, null, "Enter", null),
                new EventResponse(3, Events[2].ReceivedAt, RoomOne.Name, UserJohn.Name, null, "Comment", Events[2].Comment),
                new EventResponse(4, Events[3].ReceivedAt, RoomOne.Name, UserJane.Name, null, "Comment", Events[3].Comment),
                new EventResponse(5, Events[4].ReceivedAt, RoomOne.Name, UserJane.Name, UserJohn.Name, "HighFive", null),
                new EventResponse(6, Events[5].ReceivedAt, RoomOne.Name, UserJane.Name, null, "Leave", null),
                new EventResponse(7, Events[6].ReceivedAt, RoomOne.Name, UserJohn.Name, null, "Leave", null)
            };

            this.mapperMock.Setup(m => m.Map<IEnumerable<Event>, EventResponse[]>(Events)).Returns(expected);

            // act
            var result = this.controller.Get(year, month, day, roomId, timezoneOffset);

            // expect
            result.Should().BeOfType<OkObjectResult>();
            var actualResult = (result as OkObjectResult).Value as EventResponse[];
            actualResult.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void GetEvents_NoRoomPassed_ReturnsBadRequestResult()
        {
            // act
            var result = this.controller.Get(2020, 1, 3, null);

            // expect
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}

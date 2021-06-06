

using System.Collections.Generic;
using AutoMapper;
using ChatRoomServer.WebApi.Models;
using ChatRoomService.Domain.Models;
using ChatRoomService.WebApi;
using FluentAssertions;
using Xunit;

namespace ChatRoomServer.WebApi.Tests
{
    public class MappingProfileTest
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void MapEventSummary_Success_ReturnsGroupedEventSummary()
        {
            // arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            EventSummary[] summary = new[] {
                new EventSummary(EventType.Enter, 10, 2, 0),
                new EventSummary(EventType.Comment, 10, 4, 0),
                new EventSummary(EventType.HighFive, 10, 1, 2),
                new EventSummary(EventType.Leave, 10, 1, 0),
                new EventSummary(EventType.Enter, 11, 3, 0),
                new EventSummary(EventType.Comment, 11, 5, 0),
                new EventSummary(EventType.HighFive, 11, 2, 3),
                new EventSummary(EventType.Leave, 11, 2, 0),
            };

            var expected = new HourlyEventSummaryResponse[] {
                new HourlyEventSummaryResponse(10, new HourlyEventSummaryItem[] {
                        new HourlyEventSummaryItem ("Enter", 2, 0),
                        new HourlyEventSummaryItem ("Comment", 4, 0),
                        new HourlyEventSummaryItem ("HighFive", 1, 2),
                        new HourlyEventSummaryItem ("Leave", 1, 0)
                }),
                new HourlyEventSummaryResponse(11, new HourlyEventSummaryItem[] {
                        new HourlyEventSummaryItem ("Enter", 3, 0),
                        new HourlyEventSummaryItem ("Comment", 5, 0),
                        new HourlyEventSummaryItem ("HighFive", 2, 3),
                        new HourlyEventSummaryItem ("Leave", 2, 0)
                })
            };

            // act
            var actual = mapper.Map<IEnumerable<EventSummary>, HourlyEventSummaryResponse[]>(summary);

            // assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}

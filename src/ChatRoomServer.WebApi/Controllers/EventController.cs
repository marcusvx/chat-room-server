using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ChatRoomServer.Domain.Repositories;
using ChatRoomServer.WebApi.Models;
using ChatRoomService.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatRoomServer.WebApi.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<EventController> logger;

        public EventController(
            ILogger<EventController> logger,
            IEventRepository repository,
            IMapper mapper
        )
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{year}/{month}/{day}/hourly")]
        public IActionResult GetHourlySummary(
            int year,
            int month,
            int day,
            [FromQuery(Name = "room")] int? roomId,
            [FromHeader(Name = "X-Timezone-Offset")] int timezoneOffset = 0)
        {
            if (roomId == null)
            {
                return BadRequest("Room Id is required");
            }

            var dateTimeOffset = new DateTimeOffset(
                new DateTime(year, month, day),
                new TimeSpan(timezoneOffset, 0, 0));
            var summary = this.repository.GetHourlySummary(dateTimeOffset, roomId.Value);
            var result = this.mapper.Map<IEnumerable<EventSummary>, HourlyEventSummaryResponse[]>(summary);

            return Ok(result);
        }

        [HttpGet]
        [Route("{year}/{month}/{day}")]
        public IActionResult Get(
           int year,
           int month,
           int day,
           [FromQuery(Name = "room")] int? roomId,
           [FromHeader(Name = "X-Timezone-Offset")] int timezoneOffset = 0)
        {
            if (roomId == null)
            {
                return BadRequest("Room Id is required");
            }

            var dateTimeOffset = new DateTimeOffset(
                new DateTime(year, month, day),
                new TimeSpan(timezoneOffset, 0, 0));
            var events = this.repository.GetEvents(dateTimeOffset, roomId.Value);
            var result = this.mapper.Map<IEnumerable<Event>, EventResponse[]>(events);

            return Ok(result);
        }
    }
}

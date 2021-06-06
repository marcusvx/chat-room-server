using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ChatRoomServer.WebApi.Models;
using ChatRoomService.Domain.Models;

namespace ChatRoomService.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomResponse>();
            CreateMap<Event, EventResponse>();
            CreateMap<IEnumerable<EventSummary>, HourlyEventSummaryResponse[]>().ConvertUsing((source, dest) =>
            {
                var result = new List<HourlyEventSummaryResponse>();
                return source
                    .GroupBy(x => x.EventHour)
                    .Select(g => new HourlyEventSummaryResponse(
                        hour: g.Key,
                        items: g.Select(x => new HourlyEventSummaryItem
                        (
                            eventType: x.EventType.ToString(),
                            eventCount: x.EventCount,
                            userCount: x.UserCount
                        ))
                    ))
                    .ToArray();
            });
        }
    }
}

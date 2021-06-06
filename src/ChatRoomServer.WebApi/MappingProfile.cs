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
            CreateMap<IEnumerable<EventSummary>, HourlyEventSummaryResponse[]>().ConvertUsing((source, dest) =>
            {
                var result = new List<HourlyEventSummaryResponse>();
                return source
                    .GroupBy(x => x.EventHour)
                    .Select(g => new HourlyEventSummaryResponse
                    {
                        Hour = g.Key,
                        Items = g.Select(x => new HourlyEventSummaryItem
                        {
                            EventType = x.EventType.ToString(),
                            EventCount = x.EventCount,
                            UserCount = x.UserCount
                        })
                    })
                    .ToArray();
            });
        }
    }
}

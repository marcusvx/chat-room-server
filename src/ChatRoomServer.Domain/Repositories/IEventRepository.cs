using System;
using System.Collections.Generic;
using ChatRoomService.Domain.Models;

namespace ChatRoomServer.Domain.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<EventSummary> GetHourlySummary(DateTimeOffset date, int roomId);

        IEnumerable<Event> GetEvents(DateTimeOffset date, int roomId);
    }
}

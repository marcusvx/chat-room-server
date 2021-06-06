using System;
using System.Collections.Generic;
using ChatRoomService.Domain.Models;

namespace ChatRoomServer.Domain.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<EventSummary> GetHourlySummary(DateTime date, int roomId);

        IEnumerable<Event> GetEvents(DateTime date, int roomId);
    }
}

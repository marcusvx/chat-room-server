using System;

namespace ChatRoomService.Domain.Models
{
    public class EventSummary
    {
        public EventSummary(
            EventType eventType,
            int eventHour,
            int eventCount,
            int userCount)
        {
            this.EventType = eventType;
            this.EventHour = eventHour;
            this.EventCount = eventCount;
            this.UserCount = userCount;
        }

        public EventType EventType { get; private set; }

        public int EventHour { get; private set; }

        public int EventCount { get; private set; }

        public int UserCount { get; private set; }
    }
}

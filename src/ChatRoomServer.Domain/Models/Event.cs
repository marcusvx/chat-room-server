using System;

namespace ChatRoomService.Domain.Models
{
    public class Event
    {
        public uint Id { get; set; }

        public User FromUser { get; set; }

        public User ToUser { get; set; }

        public DateTimeOffset ReceivedAt { get; set; }

        public Room Room { get; set; }

        public EventType EventType { get; set; }

        public string Comment { get; set; }
    }
}

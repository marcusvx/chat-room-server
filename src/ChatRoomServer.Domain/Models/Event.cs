using System;

namespace ChatRoomService.Domain.Models
{
    public class Event
    {
        public Event(
            uint id,
            User fromUser,
            User toUser,
            DateTimeOffset receivedAt,
            Room room,
            EventType type,
            string comment
        )
        {
            this.Id = id;
            this.FromUser = fromUser;
            this.ToUser = toUser;
            this.ReceivedAt = receivedAt;
            this.Room = room;
            this.EventType = type;
            this.Comment = comment;
        }

        public uint Id { get; private set; }

        public User FromUser { get; private set; }

        public User ToUser { get; private set; }

        public DateTimeOffset ReceivedAt { get; private set; }

        public Room Room { get; private set; }

        public EventType EventType { get; private set; }

        public string Comment { get; private set; }
    }
}

using System;
using Newtonsoft.Json;

namespace ChatRoomServer.WebApi.Models
{
    public class EventResponse
    {
        public EventResponse(
            uint id,
            DateTimeOffset receivedAt,
            string roomName,
            string fromUserName,
            string toUserName,
            string eventType,
            string comment
        )
        {
            this.Id = id;
            this.ReceivedAt = receivedAt;
            this.RoomName = roomName;
            this.FromUserName = fromUserName;
            this.ToUserName = toUserName;
            this.EventType = eventType;
            this.Comment = comment;
        }

        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("received_at")]
        public DateTimeOffset ReceivedAt { get; set; }

        [JsonProperty("room_name")]
        public string RoomName { get; set; }

        [JsonProperty("from_user_name")]
        public string FromUserName { get; set; }

        [JsonProperty("to_user_name")]
        public string ToUserName { get; set; }

        [JsonProperty("event_type")]
        public string EventType { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}

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

        [JsonProperty("receivedAt")]
        public DateTimeOffset ReceivedAt { get; set; }

        [JsonProperty("roomName")]
        public string RoomName { get; set; }

        [JsonProperty("fromUserName")]
        public string FromUserName { get; set; }

        [JsonProperty("toUserName")]
        public string ToUserName { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}

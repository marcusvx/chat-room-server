using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatRoomServer.WebApi.Models
{
    public class HourlyEventSummaryItem
    {
        public HourlyEventSummaryItem(string eventType, int eventCount, int userCount)
        {
            this.EventType = eventType;
            this.EventCount = eventCount;
            this.UserCount = userCount;
        }

        [JsonProperty("eventType")]
        public string EventType { get; private set; }

        [JsonProperty("eventCount")]
        public int EventCount { get; private set; }

        [JsonProperty("userCount")]
        public int UserCount { get; private set; }
    }

    public class HourlyEventSummaryResponse
    {
        public HourlyEventSummaryResponse(int hour, IEnumerable<HourlyEventSummaryItem> items)
        {
            this.Hour = hour;
            this.Items = items;
        }

        [JsonProperty("hour")]
        public int Hour { get; private set; }

        [JsonProperty("items")]
        public IEnumerable<HourlyEventSummaryItem> Items { get; private set; }
    }
}

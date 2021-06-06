using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatRoomServer.WebApi.Models
{
    public class HourlyEventSummaryItem
    {
        [JsonProperty("event_type")]
        public string EventType { get; set; }

        [JsonProperty("event_count")]
        public int EventCount { get; set; }

        [JsonProperty("user_count")]
        public int UserCount { get; set; }
    }

    public class HourlyEventSummaryResponse
    {
        [JsonProperty("hour")]
        public int Hour { get; set; }

        [JsonProperty("items")]
        public IEnumerable<HourlyEventSummaryItem> Items { get; set; }
    }
}

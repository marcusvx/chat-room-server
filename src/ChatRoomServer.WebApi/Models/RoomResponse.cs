using Newtonsoft.Json;

namespace ChatRoomServer.WebApi.Models
{
    public class RoomResponse
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

using Newtonsoft.Json;

namespace ChatRoomServer.WebApi.Models
{
    public class CategoryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

using Newtonsoft.Json;

namespace Infrastructure.Integrations.Typicode.Contracts.Response.v1
{
    public class TypicodeAlbumResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
